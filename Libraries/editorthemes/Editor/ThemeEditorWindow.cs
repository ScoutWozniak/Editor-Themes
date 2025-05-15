using Editor;
using Sandbox;
using System;
using System.Collections.Generic;
public class EditorTheme
{
	public Color TabBackground { get; set; }
	public Color TabBarBackground { get; set; }
	public Color TabInactiveBackground { get; set; }
	public Color SurfaceBackground { get; set; }
	public Color SurfaceLightBackground { get; set; }
	public Color SidebarBackground { get; set; }
	public Color WindowBackground { get; set; }
	public Color WidgetBackground { get; set; }
	public Color ControlBackground { get; set; }
	public Color ButtonBackground { get; set; }
	public Color SelectedBackground { get; set; }
	public Color StatusBarBackground { get; set; }
	public Color Text { get; set; }
	public Color TextControl { get; set; }
	public Color TextLight { get; set; }
	public Color TextWidget { get; set; }
	public Color TextButton { get; set; }
	public Color TextSelected { get; set; }
	public Color TextLink { get; set; }
	public Color TextHighlight { get; set; }
	public Color TextDisabled { get; set; }
	public Color Border { get; set; }
	public Color BorderLight { get; set; }
	public Color BorderButton { get; set; }
	public Color Shadow { get; set; }
	public Color Primary { get; set; }
	public Color Overlay { get; set; }
	public Color MultipleValues { get; set; }
	public Color Highlight { get; set; }

	public int RowHeight { get; set; }
	public int ControlHeight { get; set; }
	public int ControlRadius { get; set; }

	public EditorTheme()
	{
		SetDefaults();
	}

	public EditorTheme( bool defaults )
	{
		if ( defaults )
		{
			SetDefaults();
		}
	}

	public void SetDefaults()
	{
		TabBackground = Color.Parse( "#3b3b3b" ).Value;
		TabBarBackground = Color.Parse( "#242424" ).Value;
		TabInactiveBackground = Color.Parse( "#242424" ).Value;
		SurfaceBackground = Color.Parse( "#3b3b3b" ).Value;
		SurfaceLightBackground = Color.Parse( "#696969" ).Value;
		SidebarBackground = Color.Parse( "#242424" ).Value;
		WindowBackground = Color.Parse( "#181818" ).Value;
		WidgetBackground = Color.Parse( "#242424" ).Value;
		ControlBackground = Color.Parse( "#181818" ).Value;
		ButtonBackground = Color.Parse( "#3B3B3B" ).Value;
		SelectedBackground = Color.Parse( "#808080" ).Value;
		StatusBarBackground = Color.Parse( "#242424" ).Value;
		Text = Color.Parse( "#FFFFFF" ).Value;
		TextControl = Color.Parse( "#FFFFFF" ).Value;
		TextLight = Color.Parse( "#9E9E9E" ).Value;
		TextWidget = Color.Parse( "#FFFFFF" ).Value;
		TextButton = Color.Parse( "#FFFFFF" ).Value;
		TextSelected = Color.Parse( "#66a3ff" ).Value;
		TextLink = Color.Parse( "#66a3ff" ).Value;
		TextHighlight = Color.Parse( "#66a3ff" ).Value;
		TextDisabled = Color.Parse( "#FFFFFF77" ).Value;
		Border = Color.Parse( "#525252" ).Value;
		BorderLight = Color.Parse( "#696969" ).Value;
		BorderButton = Color.Parse( "#696969" ).Value;
		Shadow = Color.Parse( "#242424" ).Value;
		Primary = Color.Parse( "#5a8deb" ).Value;
		Overlay = Color.Parse( "#242424" ).Value;
		MultipleValues = Color.Parse( "#808080" ).Value;
		Highlight = Color.Parse( "#4c7abf" ).Value;

		RowHeight = 22;
		ControlHeight = 22;
		ControlRadius = 4;
	}

	public static EditorTheme ShallowCopy( EditorTheme source )
	{
		if ( source == null )
			throw new ArgumentNullException( nameof( source ) );

		EditorTheme copy = new EditorTheme();
		var properties = typeof( EditorTheme ).GetProperties();

		foreach ( var prop in properties )
		{
			if ( prop.CanRead && prop.CanWrite )
			{
				var value = prop.GetValue( source, null );
				prop.SetValue( copy, value, null );
			}
		}

		return copy;
	}
}


public class ThemeEditorWidget : Widget
{
	ControlSheet MainSheet;
	EditorTheme theme;

	EditorThemeResource LoadedResource { get; set; }

	Layout ResourceLayout;


	[ResourceType( "etheme" )] string ThemeResourcePath { get; set; } = "";

	public ThemeEditorWidget( Widget parent ) : base( parent, false )
	{

		theme = LoadTheme();

		WindowTitle = "Theme Editor";



		// Create a Column Layout
		Layout = Layout.Column();
		// Give it some Margins/Spacing
		Layout.Margin = 4;
		Layout.Spacing = 4;
		Layout.Alignment = TextFlag.Center;
		Size = new Vector2( 400, 600 );

		ResourceLayout = Layout.AddRow();



		TranslucentBackground = true;
		WindowOpacity = 0.99f;

		var themeResourceFinder = new ResourceControlWidget( this.GetSerialized().GetProperty( "LoadedResource" ) );
		themeResourceFinder.Parent = this;
		themeResourceFinder.MaximumWidth = 300;
		ResourceLayout.Add( themeResourceFinder );

		var applyTheme = ResourceLayout.Add( new Button( "Apply Resource", "casino", this ) );
		applyTheme.Clicked += () =>
		{
			if ( LoadedResource.IsValid() )
			{
				theme = EditorTheme.ShallowCopy( LoadedResource.ETheme );
				SaveTheme( theme );
				ApplyToEditor( theme );
				RebuildThemes();
				Refresh();
			}

		};



		var button = Layout.Add( new Button( "Save To Editor", "", this ) );
		button.Clicked += () =>
		{
			SaveTheme( theme );
			ApplyToEditor( theme );
			Refresh();
		};

		var resetButton = Layout.Add( new Button( "Reset", "casino", this ) );
		resetButton.Clicked += () =>
		{
			ResetToDefault( theme );
		};



		Label colorLabel = new Label( "Colors", this );
		colorLabel.Alignment = TextFlag.Center;
		colorLabel.SetStyles( $"font-weight: 600; color: {Editor.Theme.Text}; text-align: center;" );

		Layout.Add( colorLabel );

		MainSheet = new ControlSheet();
		MainSheet.Alignment = TextFlag.Center;
		Layout.Add( MainSheet );
		RebuildThemes();
		Refresh();
	}

	void Refresh()
	{
		SetStyles( $"color: {Editor.Theme.Text.Hex}; color: white; font-weight: 600;" );
	}

	void RebuildThemes()
	{
		MainSheet.Clear( true );
		MainSheet.AddObject( theme.GetSerialized() );
	}

	public static EditorTheme LoadTheme()
	{
		EditorTheme theme = new();
		Dictionary<string, string> dictColors = Editor.FileSystem.Root.ReadJson<Dictionary<string, string>>( "addons/tools/assets/styles/theme.json" );
		foreach ( var col in dictColors )
		{
			if ( col.Key == "RowHeight" || col.Key == "ControlHeight" || col.Key == "ControlRadius" )
			{
				theme.GetType().GetProperty( col.Key )?.SetValue( theme, col.Value.ToInt() );
				continue;
			}
			var color = Color.Parse( col.Value );
			if ( col.Key == "" || color == null )
			{
				Log.Error( $"Failed to parse color for {col.Key} with value {col.Value}" );
				continue;
			}
			theme.GetType().GetProperty( col.Key )?.SetValue( theme, color.Value );
		}

		return theme;
	}



	public static void SaveTheme( EditorTheme theme )
	{
		Dictionary<string, string> dictColors = new();
		foreach ( var prop in theme.GetType().GetProperties() )
		{
			if ( prop.Name == "RowHeight" || prop.Name == "ControlHeight" || prop.Name == "ControlRadius" )
			{
				dictColors.Add( prop.Name, prop.GetValue( theme ).ToString() );
				continue;
			}
			if ( prop.PropertyType == typeof( Color ) )
			{
				dictColors.Add( prop.Name, ((Color)prop.GetValue( theme )).Hex );
			}
		}
		Editor.FileSystem.Root.WriteJson( "addons/tools/assets/styles/theme.json", dictColors );
	}

	public static void ApplyToEditor( EditorTheme theme )
	{
		foreach ( var property in theme.GetType().GetProperties() )
		{
			var themeProp = typeof( Editor.Theme ).GetField( property.Name );
			if ( themeProp != null )
			{
				themeProp.SetValue( null, property.GetValue( theme ) );
			}

		}


	}

	public static void ResetToDefault( EditorTheme theme )
	{
		theme.SetDefaults();
	}
}

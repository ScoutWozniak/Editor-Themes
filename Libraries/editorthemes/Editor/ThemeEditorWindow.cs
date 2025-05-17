using Editor;
using Sandbox;
using System.Collections.Generic;

[Dock( "Editor", "Theme Editor", "local_fire_department" )]
public class ThemeEditorWidget : Widget
{
	ControlSheet MainSheet;
	EditorTheme theme;

	EditorThemeResource LoadedResource { get; set; }

	Layout ResourceLayout;

	Layout ThemeLayout { get; set; }

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
		Layout.AddStretchCell();

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



		var button = Layout.Add( new Button( "Save", "", this ) );
		button.Clicked += () =>
		{
			SaveTheme( theme, LoadedResource );
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

		ThemeLayout = Layout.Column();

		MainSheet = new ControlSheet();
		RebuildThemes();
		ThemeLayout.SizeConstraint = SizeConstraint.SetFixedSize;


		var scrollArea = Layout.Add( new ScrollArea( this ) { Canvas = new Widget { Layout = Layout.Column() } }, 1 );
		var scrollLayout = scrollArea.Canvas.Layout;

		scrollLayout.Add( MainSheet );
		scrollLayout.Spacing = 4;

		Refresh();

		Layout.Add( ThemeLayout );
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
			if ( col.Key == "DefaultFont" || col.Key == "HeadingFont" )
			{
				theme.GetType().GetProperty( col.Key )?.SetValue( theme, col.Value );
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



	public static void SaveTheme( EditorTheme theme, EditorThemeResource loadedResource = null )
	{
		Dictionary<string, string> dictColors = new();
		foreach ( var prop in theme.GetType().GetProperties() )
		{
			if ( prop.Name == "RowHeight" || prop.Name == "ControlHeight" || prop.Name == "ControlRadius" || prop.Name == "DefaultFont" || prop.Name == "HeadingFont" )
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

		if ( loadedResource.IsValid() )
		{
			loadedResource.ETheme = theme;
		}
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
		theme = new EditorTheme();
	}
}

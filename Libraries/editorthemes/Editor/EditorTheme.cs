using Sandbox;
using System;

public class EditorTheme
{
	[Group( "Window" )] public Color TabBackground { get; set; } = Color.Parse( "#3b3b3b" ).Value;
	[Group( "Window" )] public Color TabBarBackground { get; set; } = Color.Parse( "#242424" ).Value;
	[Group( "Window" )] public Color TabInactiveBackground { get; set; } = Color.Parse( "#242424" ).Value;
	[Group( "Window" )] public Color SurfaceBackground { get; set; } = Color.Parse( "#3b3b3b" ).Value;
	[Group( "Window" )] public Color SurfaceLightBackground { get; set; } = Color.Parse( "#696969" ).Value;
	[Group( "Window" )] public Color SidebarBackground { get; set; } = Color.Parse( "#242424" ).Value;
	[Group( "Window" )] public Color WindowBackground { get; set; } = Color.Parse( "#181818" ).Value;
	[Group( "Window" )] public Color WidgetBackground { get; set; } = Color.Parse( "#242424" ).Value;
	[Group( "Window" )] public Color ControlBackground { get; set; } = Color.Parse( "#181818" ).Value;
	[Group( "Window" )] public Color ButtonBackground { get; set; } = Color.Parse( "#3B3B3B" ).Value;
	[Group( "Window" )] public Color SelectedBackground { get; set; } = Color.Parse( "#808080" ).Value;
	[Group( "Window" )] public Color StatusBarBackground { get; set; } = Color.Parse( "#242424" ).Value;
	[Group( "Text" )] public Color Text { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	[Group( "Text" )] public Color TextControl { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	[Group( "Text" )] public Color TextLight { get; set; } = Color.Parse( "#9E9E9E" ).Value;
	[Group( "Text" )] public Color TextWidget { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	[Group( "Text" )] public Color TextButton { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	[Group( "Text" )] public Color TextSelected { get; set; } = Color.Parse( "#66a3ff" ).Value;
	[Group( "Text" )] public Color TextLink { get; set; } = Color.Parse( "#66a3ff" ).Value;
	[Group( "Text" )] public Color TextHighlight { get; set; } = Color.Parse( "#66a3ff" ).Value;
	[Group( "Text" )] public Color TextDisabled { get; set; } = Color.Parse( "#FFFFFF77" ).Value;
	[Group( "Text" )] public Color TextDark { get; set; } = Color.Parse( "#000000" ).Value;
	[Group( "Window" )] public Color Border { get; set; } = Color.Parse( "#525252" ).Value;
	[Group( "Window" )] public Color BorderLight { get; set; } = Color.Parse( "#696969" ).Value;
	[Group( "Window" )] public Color BorderButton { get; set; } = Color.Parse( "#696969" ).Value;
	[Group( "Window" )] public Color Shadow { get; set; } = Color.Parse( "#242424" ).Value;
	[Group( "Window" )] public Color Primary { get; set; } = Color.Parse( "#5a8deb" ).Value;
	[Group( "Window" )] public Color Overlay { get; set; } = Color.Parse( "#242424" ).Value;
	[Group( "Window" )] public Color MultipleValues { get; set; } = Color.Parse( "#808080" ).Value;
	[Group( "Window" )] public Color Highlight { get; set; } = Color.Parse( "#4c7abf" ).Value;

	[Group( "Checkbox" )] public Color ToggleEnabled { get; set; } = Color.Parse( "#5aeb5c" ).Value;
	[Group( "Checkbox" )] public Color ToggleDisabled { get; set; } = Color.Parse( "#566e56" ).Value;

	[Group( "Colors?" )] public Color Blue { get; set; } = Color.Parse( "#5a8deb" ).Value;

	[Group( "Colors?" )] public Color Green { get; set; } = Color.Parse( "#B0E24D" ).Value;

	[Group( "Colors?" )] public Color Red { get; set; } = Color.Parse( "#FB5A5A" ).Value;

	[Group( "Colors?" )] public Color Yellow { get; set; } = Color.Parse( "#E6DB74" ).Value;

	[Group( "Colors?" )] public Color Pink { get; set; } = Color.Parse( "#DF9194" ).Value;

	[Group( "Files" )] public Color Prefab { get; set; } = Color.Parse( "#5a8deb" ).Value;

	[Group( "Files" )] public Color Folder { get; set; } = Color.Parse( "#E6DB74" ).Value;

	[Group( "Layout" )] public int RowHeight { get; set; } = 22;
	[Group( "Layout" )] public int ControlHeight { get; set; } = 22;
	[Group( "Layout" )] public int ControlRadius { get; set; } = 4;

	[FontName][Group( "Text" )] public string HeadingFont { get; set; } = "Inter";

	[FontName][Group( "Text" )] public string DefaultFont { get; set; } = "Inter";

	public EditorTheme()
	{ }

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

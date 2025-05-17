using System;

public class EditorTheme
{
	public Color TabBackground { get; set; } = Color.Parse( "#3b3b3b" ).Value;
	public Color TabBarBackground { get; set; } = Color.Parse( "#242424" ).Value;
	public Color TabInactiveBackground { get; set; } = Color.Parse( "#242424" ).Value;
	public Color SurfaceBackground { get; set; } = Color.Parse( "#3b3b3b" ).Value;
	public Color SurfaceLightBackground { get; set; } = Color.Parse( "#696969" ).Value;
	public Color SidebarBackground { get; set; } = Color.Parse( "#242424" ).Value;
	public Color WindowBackground { get; set; } = Color.Parse( "#181818" ).Value;
	public Color WidgetBackground { get; set; } = Color.Parse( "#242424" ).Value;
	public Color ControlBackground { get; set; } = Color.Parse( "#181818" ).Value;
	public Color ButtonBackground { get; set; } = Color.Parse( "#3B3B3B" ).Value;
	public Color SelectedBackground { get; set; } = Color.Parse( "#808080" ).Value;
	public Color StatusBarBackground { get; set; } = Color.Parse( "#242424" ).Value;
	public Color Text { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	public Color TextControl { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	public Color TextLight { get; set; } = Color.Parse( "#9E9E9E" ).Value;
	public Color TextWidget { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	public Color TextButton { get; set; } = Color.Parse( "#FFFFFF" ).Value;
	public Color TextSelected { get; set; } = Color.Parse( "#66a3ff" ).Value;
	public Color TextLink { get; set; } = Color.Parse( "#66a3ff" ).Value;
	public Color TextHighlight { get; set; } = Color.Parse( "#66a3ff" ).Value;
	public Color TextDisabled { get; set; } = Color.Parse( "#FFFFFF77" ).Value;

	public Color TextDark { get; set; } = Color.Parse( "#000000" ).Value;
	public Color Border { get; set; } = Color.Parse( "#525252" ).Value;
	public Color BorderLight { get; set; } = Color.Parse( "#696969" ).Value;
	public Color BorderButton { get; set; } = Color.Parse( "#696969" ).Value;
	public Color Shadow { get; set; } = Color.Parse( "#242424" ).Value;
	public Color Primary { get; set; } = Color.Parse( "#5a8deb" ).Value;
	public Color Overlay { get; set; } = Color.Parse( "#242424" ).Value;
	public Color MultipleValues { get; set; } = Color.Parse( "#808080" ).Value;
	public Color Highlight { get; set; } = Color.Parse( "#4c7abf" ).Value;

	public Color ToggleEnabled { get; set; } = Color.Parse( "#5aeb5c" ).Value;
	public Color ToggleDisabled { get; set; } = Color.Parse( "#566e56" ).Value;

	public Color Blue { get; set; } = Color.Parse( "#5a8deb" ).Value;

	public Color Green { get; set; } = Color.Parse( "#B0E24D" ).Value;

	public Color Red { get; set; } = Color.Parse( "#FB5A5A" ).Value;

	public Color Yellow { get; set; } = Color.Parse( "#E6DB74" ).Value;

	public Color Pink { get; set; } = Color.Parse( "#DF9194" ).Value;

	public Color Prefab { get; set; } = Color.Parse( "#5a8deb" ).Value;

	public Color Folder { get; set; } = Color.Parse( "#E6DB74" ).Value;

	public int RowHeight { get; set; } = 22;
	public int ControlHeight { get; set; } = 22;
	public int ControlRadius { get; set; } = 4;

	public string HeadingFont { get; set; } = "Inter";

	public string DefaultFont { get; set; } = "Inter";

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

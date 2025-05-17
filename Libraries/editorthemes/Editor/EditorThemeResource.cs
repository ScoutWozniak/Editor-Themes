
using Sandbox;

[GameResource( "Editor Theme", "etheme", "Theme for the editor", Icon = "pregnant_woman", IconBgColor = "#897ecf", IconFgColor = "#26214a" )]
public class EditorThemeResource : GameResource
{
	public string Name { get; set; } = "Custom Editor Theme";
	[TextArea]
	public string Description { get; set; } = "A custom editor theme made by me!";

	[InlineEditor]
	public EditorTheme ETheme { get; set; } = new();
}

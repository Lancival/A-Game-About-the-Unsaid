using System.Collections;
using System.Collections.Generic;

static class CharacterInfo {
	public static readonly string[] names =
		{
			"June",		// ID = 0, main character
			"Miles",	// ID = 1, character_description
			"Rebecca",	// ID = 2, best friend
			"ERROR"		// ID = 3, ERROR!!!!
		};
}

static class Settings {
	public static float DIALOGUE_SPEED = 0.03f;
	public static bool PAUSED = false;
	public static float AUDIO_VOLUME = 1.0f;
	public static int FONT_SCALE = 1; // 0 = small, 1 = normal, 2 = large
}

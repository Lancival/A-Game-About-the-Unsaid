using System.Collections;
using System.Collections.Generic;

static class CharacterInfo {
	public static readonly string[] names =
		{
			"Player",	// ID = 0
			"MainNPC",	// ID = 1
			"ERROR"		// ID = 2
		};
}

static class Settings {
	public static float DIALOGUE_SPEED = 0.03f;
	public static bool PAUSED = false;
}

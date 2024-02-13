using System.Collections;
using System.Collections.Generic;

public static class StoryProgression
{
	public static bool russian_language_picked = false;
	public static int day_count = 1;
	public static bool first_night = true;  //change to false in editing mode, change to true at release
	public static bool met_hinie_before = false;
	public static bool saw_sand_whales = false;
	public static bool met_walers = false;
	public static bool helped_whalers = false;
	public static bool met_horned = false;
	public static bool helped_horned = false;
	public static int rake_hits = 0;
	public static bool rake_comprehended = false;
	public static bool target_range_passed = false;
	public static bool met_friendly_crab = false;
	public static bool healed_friendly_crab = false;
	public static bool used_statue = false;

	public static bool has_self_controlled = false;

	public static void ResetStoryProgression()
	{
		day_count = 1;
		first_night = true;
		saw_sand_whales = false;
		met_walers = false;
		helped_whalers = false;
		met_horned = false;
		helped_horned = false;
		rake_hits = 0;
		rake_comprehended = false;
		target_range_passed = false;
		met_friendly_crab = false;
		healed_friendly_crab = false;
		used_statue = false;

		has_self_controlled = false;
	}
}

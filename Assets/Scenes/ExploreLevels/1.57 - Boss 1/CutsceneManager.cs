using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutsceneManager : ICustomDialogueScript
{
	public VideoPlayer video_player;
	public VideoClip cutscene_clip;
	public RenderTexture cutscene_texture;
	public RawImage video_in_canvas;
	bool cutscene_instantiated = false;

	private void Awake()
	{
		if (!Game_Mode_Definer.Gamemode_Explore())
		{
			GameObject.FindGameObjectWithTag("turn_manager").GetComponent<TurnManager>().OnBattleWon += RunFromDialogue;
		}
	}
	public override void RunFromDialogue()
	{
		if (!cutscene_instantiated)
		{
			video_player.gameObject.SetActive(true);
			video_player.clip = cutscene_clip;
			RawImage started_cutscene = Instantiate(video_in_canvas, GameObject.FindGameObjectWithTag("pause_menu").transform);
			started_cutscene.texture = cutscene_texture;
			cutscene_instantiated = true;
		}
	}
}

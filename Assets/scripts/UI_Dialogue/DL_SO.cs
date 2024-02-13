using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Lines", menuName = "Dialogue Lines")]
[System.Serializable]
public class DL_SO : ScriptableObject
{
	[TextArea(5, 20)]
	public string[] dialogue_lines;

	[System.Serializable]
	public struct AnswerOptions { public Answer_Class[] answers;}

	[SerializeField]
	public AnswerOptions[] answer_options;

	public Answer_Class[] ReturnAnswerOptions(int _dialogue_line_ID)
	{
		foreach (AnswerOptions option in answer_options)
		{
			if (option.answers[0].corelated_dialogue_line_ID == _dialogue_line_ID)
			{
				return option.answers;
			}
		}

		return new Answer_Class[] { new Answer_Class("Contiune", 0) };
	}
}



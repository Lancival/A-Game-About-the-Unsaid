using System.Collections;
using System.Collections.Generic;
using System;

public class Dialogue {
	private int dialogueID; // ID # of this dialogue node
	private int speakerID; // ID # of speaker
	private string content; // Dialogue lines
	private List<int> links; // ID #'s of dialogue nodes representing responses

	private const char separator = ';';

	public Dialogue(int dID, int sID, string text, List<int> l) {
		dialogueID = dID;
		speakerID = sID;
		content = text;
		links = l;
	}

	public int getDialogueID() {
		return dialogueID;
	}

	public int getSpeakerID() {
		return speakerID;
	}

	public string getText() {
		return content;
	}

	public List<int> getResponseID() {
		return links;
	}

	// Convert from semi-colon separated values into dialogue objects
	public static List<Dialogue> extract(string text) {
		List<Dialogue> nodes = new List<Dialogue>();
		string[] lines = text.Split('\n');
		foreach (string line in lines) {
			string[] fields = line.Split(';');
			nodes.Add(new Dialogue(int.Parse(fields[0]), int.Parse(fields[1]), fields[2], new List<int>(Array.ConvertAll(fields[3].Split(','), Convert.ToInt32))));
		}
		return nodes;
	}
}

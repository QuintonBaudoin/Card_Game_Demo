﻿using UnityEngine;
using System.Collections;

public class TreasureCardMono : CardMono<TreasureCard>
{
	public int _gold;

	public override void Init()
	{
		theCard = new TreasureCard(Name, Description, _gold);
	}

	public void Init(string n, string d)
	{
		theCard = new TreasureCard (n, d, _gold);
	}

	public void Init(iCard c)
	{
		theCard = new TreasureCard (c.Name, c.Description, _gold);
	}



}
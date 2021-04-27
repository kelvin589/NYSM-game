using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
	public TMP_Text text;

	public RoomInfo info;

	/// <summary>
	/// Provides the relevent information for room setup.
	/// </summary>

	public void SetUp(RoomInfo _info)
	{
		info = _info;
		text.text = _info.Name;
	}

	/// <summary>
	/// Join the Room (RoomListItems)
	/// </summary>

	public void OnClick()
	{
		Launcher.Instance.JoinRoom(info);
	}
}
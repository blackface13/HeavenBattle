using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class User
{
	//JSON
	public string username = "";
	public string deviceId = "";
	public string pw = "";
	//NON JSON
}

//[SocketIO] Socket.IO sid: {"sid":"qoYoimy5G0Nexg-JAAAD","upgrades":[],"pingInterval":25000,"pingTimeout":5000}
[Serializable]
public class SocketInfo
{
	//JSON
	public string sid = "";
	public int pingInterval;
	public int pingTimeout;
	//NON JSON
}

[Serializable]
public class Message
{
	//JSON
	public string message = "";
	//NON JSON
}
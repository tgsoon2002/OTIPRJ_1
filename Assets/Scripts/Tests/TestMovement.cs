using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;

public class TestMovement : MonoBehaviour
{

	private Stack<CharacterInputs> prevInput;
	private float direction;
	private Rigidbody physics;
	public int isDashing = 0;

	int tap = 0;
	bool startTimer = false;
	float begin;
	float lastTap;
	// Use this for initialization
	void Start () 
	{
		prevInput = new Stack<CharacterInputs>();
		physics = gameObject.GetComponent<Rigidbody>();
		direction = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ProcessCommand (float [] inputs) {

		//TO DO FOR LATER: Put all the Stack.Pop commands to the else.
		foreach (CharacterInputs cmd in Enum.GetValues(typeof(CharacterInputs))) 
		{
			float temp_Input_Value = inputs [(int)cmd];

			if(temp_Input_Value != 0)
			{
				if(temp_Input_Value == 1)
				{
					if(cmd == CharacterInputs.Character_Move_Left)
					{
						if(!startTimer)
						{
							begin = Time.time;
							startTimer = true;
						}
							
						float temp = 0.0f;
						float temp2 = 0.0f;
						//temp  = begin;
						temp = Time.time - lastTap;
						temp2 = Time.time - begin;
						//Debug.Log(temp);
						Debug.Log(tap);
						//temp < 1.0f  &&
						if( temp2 > 0.5f && tap <= -0.5f)
						{
							Debug.Log("Sprint...");

							physics.AddForce(Vector3.left * 100.0f);
							prevInput.Push(CharacterInputs.Character_Move_Left);

							tap = -2;
						}
						else
						{
							physics.AddForce(Vector3.left * 20.0f);
							prevInput.Push(CharacterInputs.Character_Move_Left);
							//tap = false;
						}
					}
					else if (cmd == CharacterInputs.Character_Move_Right) 
					{
						if(!startTimer)
						{
							begin = Time.time;
							startTimer = true;
						}

						float temp = 0.0f;
						float temp2 = 0.0f;
						//temp  = begin;
						temp = Time.time - lastTap;
						temp2 = Time.time - begin;
						//Debug.Log(temp);
						Debug.Log(tap);
						//temp < 1.0f  &&
						if( temp2 > 1.0f && tap >= 1)
						{
							Debug.Log("Sprint...");

							physics.AddForce(Vector3.right * 100.0f);
							prevInput.Push(CharacterInputs.Character_Move_Right);
							tap = 2;
						}
						else
						{
							physics.AddForce(Vector3.right * 20.0f);
							prevInput.Push(CharacterInputs.Character_Move_Right);
							//tap = false;
						}
					}
				}
				else
				{	
//					startTimer = false;
//					begin = Time.time - begin;
//
//					Debug.Log(cmd);
//					if (tap == 2 || tap == -2) {
//						tap = 0;
//					}
					if(cmd == CharacterInputs.Character_Move_Left)
					{
						startTimer = false;
						begin = Time.time - begin;

						Debug.Log(cmd);
						if (tap == 2 || tap == -2) {
							tap = 0;
						}

						if(begin < 0.3f && prevInput.Peek() == CharacterInputs.Character_Move_Left)
						{
							Debug.Log("Tapped");
							lastTap = Time.time;
							if(tap == -1)
							{
								physics.AddForce(Vector3.left * 1000.0f);
								prevInput.Pop();
								prevInput.Push(CharacterInputs.Character_Move_Left);
								tap = 0;
								Debug.Log("DASH");
							}
							else
							{
								tap = -1;
							}
						}
						prevInput.Clear();
					}
					else if (cmd == CharacterInputs.Character_Move_Right) 
					{
						startTimer = false;
						begin = Time.time - begin;

						Debug.Log(cmd);
						if (tap == 2 || tap == -2) {
							tap = 0;
						}

						if(begin < 0.3f && prevInput.Peek() == CharacterInputs.Character_Move_Right)
						{
							Debug.Log("Tapped");
							lastTap = Time.time;
							if(tap == 1)
							{
								physics.AddForce(Vector3.right * 1000.0f);
								prevInput.Pop();
								prevInput.Push(CharacterInputs.Character_Move_Right);
								tap = 0;
								Debug.Log("DASH");
							}
							else
							{
								tap = 1;
							}
						}

						prevInput.Clear();
					}
				}
			}

//			if (temp_Input_Value != 0) 
//			{
//				if(temp_Input_Value == 1)
//				{
//					if(cmd == CharacterInputs.Character_Move_Left || cmd == CharacterInputs.Character_Move_Right)
//					{
//						if(!startTimer)
//						{
//							begin = Time.time;
//							startTimer = true;
//						}
//						else
//						{
//							if(cmd == CharacterInputs.Character_Move_Left)
//							{
//								physics.AddForce(Vector3.left * 5.0f);
//							}
//							else if( cmd == CharacterInputs.Character_Move_Right)
//								physics.AddForce(Vector3.right * 5.0f);
//						}
//					}
//				}
//				else
//				{
//					startTimer = false;
//					begin = Time.time - begin;
//
//					Debug.Log(begin);
//
//					if(cmd == CharacterInputs.Character_Move_Left || cmd == CharacterInputs.Character_Move_Right)
//					{
////						if(prevInput.Count > 0 && prevInput.Peek() == CharacterInputs.Character_Move_Left && begin < 0.1f)
////						{
////							Debug.Log("TAP");
////
////
////						}
//						//prevInput.Push(CharacterInputs.Character_Move_Left);
//						tap.Add(true);
//
//
//						Debug.Log(tap.Count);
//
//						if(tap.Count > 1 && begin < 0.1f)
//						{
//							Debug.Log("Boot");
//
//							if(cmd == CharacterInputs.Character_Move_Left)
//							{
//								physics.AddForce(Vector3.left * 1000.0f);
//								tap.Clear();
//							}
//							else if(cmd == CharacterInputs.Character_Move_Right)
//							{
//								physics.AddForce(Vector3.right * 1000.0f);
//								tap.Clear();
//							}
//						}
//					}
//				}
//		
//			}
		}
	}
}

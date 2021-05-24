#pragma warning disable 0649

using System;
using System.Collections;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Watson.Assistant.V2;
using IBM.Watson.Assistant.V2.Model;
using IBM.Watson.TextToSpeech.V1;

using UnityEngine;
using UnityEngine.UI;

namespace IBM.Watson.Examples
{
    public class CommandBot: SimpleBot
    {
        //private bool messageTested = false;

        protected override void OnMessage(DetailedResponse<MessageResponse> response, IBMError error)
        {
            Debug.Log("New OnMessage");

            textResponse = response.Result.Output.Generic[0].Text.ToString();
            messageTested = true;

            if (response != null && response.Result.Output.Intents.Count != 0)
            {
                string intent = response.Result.Output.Intents[0].Intent;
                Debug.Log("Intent: " + intent);
                string currentMat = null;
                string currentScale = null;
                string direction = null;
                if (intent == "move")
                {
                    foreach (RuntimeEntity entity in response.Result.Output.Entities)
                    {
                        Debug.Log("entityType: " + entity.Entity + " , value: " + entity.Value);
                        direction = entity.Value;
                        //gameManager.MoveObject(direction);
                    }
                }

                if (intent == "create")
                {
                    bool createdObject = false;
                    
                    GameObject myObject = null;
                    Renderer rend = null;
                    string ObjectType = null;
                    Color ObjectColor = Color.red;

                    foreach (RuntimeEntity entity in response.Result.Output.Entities)
                    {
                        Debug.Log("entityType: " + entity.Entity + " , value: " + entity.Value);
                        if (entity.Entity == "object")
                        {
                            //gameManager.CreateObject(entity.Value, currentMat, currentScale);
                            createdObject = true;
                            currentMat = null;
                            currentScale = null;
                            ObjectType = entity.Value;

                        }
                        if (entity.Entity == "material")
                        {
                            currentMat = entity.Value;

                            if (currentMat == "black")
                            {
                                ObjectColor = Color.black;
                            }
                            else if (currentMat == "blue")
                            {
                                ObjectColor = Color.blue;
                            }
                            else if (currentMat == "cyan")
                            {
                                ObjectColor = Color.cyan;
                            }
                            else if (currentMat == "gray")
                            {
                                ObjectColor = Color.gray;
                            }
                            else if (currentMat == "green")
                            {
                                ObjectColor = Color.green;
                            }
                            else if (currentMat == "magenta")
                            {
                                ObjectColor = Color.magenta;
                            }
                            else if (currentMat == "red")
                            {
                                ObjectColor = Color.red;
                            }
                            else if (currentMat == "white")
                            {
                                ObjectColor = Color.white;
                            }
                            else if (currentMat == "yellow")
                            {
                                ObjectColor = Color.yellow;
                            }


                        }
                        if (entity.Entity == "scale")
                        {
                            currentScale = entity.Value;
                        }

                        if (ObjectType != null)
                        {
                            if (ObjectType == "cube")
                            {
                                myObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                myObject.transform.position = new Vector3(0f, 1f, 0f);
                                rend = myObject.GetComponent<Renderer>();
                                rend.material.color = Color.red;
                            }
                            else if (ObjectType == "ball")
                            {
                                myObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                myObject.transform.position = new Vector3(0f, 1f, 0f);
                                rend = myObject.GetComponent<Renderer>();
                                rend.material.color = Color.red;
                            }
                            rend = myObject.GetComponent<Renderer>();
                            rend.material.color = ObjectColor;
                        }
                        
                    }

                    if (!createdObject)
                    {
                        //gameManager.PlayError(sorryClip);
                    }

                }
                else if (intent == "destroy")
                {
                    //gameManager.DestroyAtPointer();
                }
                else if (intent == "help")
                {

                }
            }
            else
            {
                Debug.Log("Failed to invoke OnMessage();");
            }
        }

    }

}

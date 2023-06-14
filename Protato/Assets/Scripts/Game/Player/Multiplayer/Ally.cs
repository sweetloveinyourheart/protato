using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Ally : Character
{
    public string userId;

    private void Update()
    {
        
    }

    private async void FixedUpdate()
    {
        JSONNode res = await SocketContext.Instance.ReceiveMessageAsync();
        if (res != null)
        {
            string type = res["type"];
            JSONNode data = res["data"];

            if (type == ServerEvent.PlayerMoved)
            {
                string playerId = data["userId"];
                if (playerId == userId)
                {
                    float xPos = data["xPos"].AsFloat;
                    float yPos = data["yPos"].AsFloat;

                    Vector2 previousPos = transform.position;
                    Vector2 currentPos = new Vector2(xPos, yPos);
                    Vector2 movement = currentPos - previousPos; // check if the player is moving or not

                    transform.position = currentPos;

                    FlipCharacter(movement.x);
                    UpdateAnimation(movement);
                }
            }
        }
    }
}

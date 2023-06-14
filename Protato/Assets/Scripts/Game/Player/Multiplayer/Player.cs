using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Threading.Tasks;

public class Player : Character
{
    public string userId;

    private async void FixedUpdate()
    {
        await SendPosition();
    }

    private async Task<bool> SendPosition()
    {
        JSONObject movementObj = new JSONObject();
        movementObj["matchId"] = MatchManager.Instance.matchId;
        movementObj["xPos"] = transform.position.x;
        movementObj["yPos"] = transform.position.y;
        movementObj["userId"] = userId;

        await SocketContext.Instance.SendMessageAsync(ClientEvent.Move, movementObj);
        return true;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
    }
}

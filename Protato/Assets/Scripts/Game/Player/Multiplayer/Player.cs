using System.Collections;
using UnityEngine;
using SimpleJSON;

public class Player : Character
{
    public string userId;

    private bool isDead = false;

    private void FixedUpdate()
    {
        if (!isDead)
        {
            JSONObject movementObj = new JSONObject();
            movementObj["matchId"] = MatchManager.Instance.matchId;
            movementObj["userId"] = userId;
            movementObj["xPos"] = transform.position.x;
            movementObj["yPos"] = transform.position.y;
            movementObj["isAttacking"] = isAttacking;
            movementObj["hp"] = hp;

            SocketContext.Instance.SendMessage(ClientEvent.UpdatePlayerState, movementObj);
        }

    }

    protected override void Die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        MultiplayerManager.Instance.PlayerDead();
        StartCoroutine(GoSleep());
    }

    private IEnumerator GoSleep()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        isDead = true;
    }
}

using UnityEngine;

public class Manager : MonoBehaviour
{
    public Board board;

    private void Start()
    {
        board = Board.Load(GetComponentsInChildren<Position>());
    }

    internal void SelectPiece(Position position, Piece piece)
    {
        Debug.Log(string.Format("Select '{1}' piece in '{0}' position.", position.name, piece.name));
    }
}

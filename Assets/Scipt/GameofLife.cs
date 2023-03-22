using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameofLife : MonoBehaviour
{
    [SerializeField]
    private TextAsset _StageTextAsset;

    [SerializeField]
    private GameObject _cellPrefab;

    private CellStatus[,] _cellStatusArray;
    private Cell[,] _cellArray;

    void loadCellStatus()
    {
        string[] Lines = this._StageTextAsset.text.Split("\n");
        string[] first_words = Lines[0].Split(",");

        /*for(int i = 0; i < Lines.GetLength(0); i++)
        {
            string[] words = Lines[i].Split(",");
            for(int j = 0; j < words.GetLength(0) ; j++)
            {
                Debug.Log(words[j]);
            }
        }*/
        this._cellStatusArray = new CellStatus[Lines.Length +2, first_words.Length + 2];
        for(int j = 0; j < this._cellStatusArray.GetLength(1); j++)
        {
            this._cellStatusArray[0, j] = new CellStatus(
                false,
                true,
                false
            );
        }

        for(int i = 0; i < this._cellStatusArray.GetLength(0) - 2; i++)
        {
            this._cellStatusArray[i + 1, 0] = new CellStatus(
                false,
                true,
                false
            );

            string[] words = Lines[i].Split(",");
            for(int j = 0; j < this._cellStatusArray.GetLength(1) - 2; j++)
            {
                switch(words[j].Trim())
                {
                    case "0":
                        this._cellStatusArray[i+1, j+1] = new CellStatus(
                            false,
                            false,
                            false
                        );
                        break;
                    case "1":
                        this._cellStatusArray[i+1,j+1] = new CellStatus(
                            true,
                            false,
                            false
                        );
                        break;
                    case "2":
                        this._cellStatusArray[i+1,j+1] = new CellStatus(
                            false,
                            true,
                            false
                        );
                        break;
                    case "3":
                        this._cellStatusArray[i+1,j+1] = new CellStatus(
                            true,
                            true,
                            false
                        );
                        break;
                    case "4":
                        this._cellStatusArray[i+1,j+1] = new CellStatus(
                            false,
                            true,
                            true
                        );
                        break;
                    default:
                        this._cellStatusArray[i+1,j+1] = new CellStatus(
                            false,
                            true,
                            false
                        );
                        break;
                }
            }
            this._cellStatusArray[i + 1, this._cellStatusArray.GetLength(1) - 1] = new CellStatus(
                false,
                true,
                false
            );
        }

        for(int j = 0; j < this._cellStatusArray.GetLength(1); j++)
        {
            this._cellStatusArray[this._cellStatusArray.GetLength(0) - 1, j] = new CellStatus(
                false,
                true,
                false
            );
        }
    }

    void Start()
    {
        this.loadCellStatus();
        this.CreateCell();

    }

    void CreateCell()
    {
        this._cellArray = new Cell[this._cellStatusArray.GetLength(0), this._cellStatusArray.GetLength(1)];
        for(int i = 0; i < this._cellStatusArray.GetLength(0); i++)
        {
            for(int j = 0; j < this._cellStatusArray.GetLength(1); j++)
            {
                GameObject cell = Instantiate(this._cellPrefab);
                cell.name = string.Format("({0}, {1})", i,j);
                cell.transform.localPosition = new Vector3(j, i, 0);
                this._cellArray[i,j] = cell.GetComponent<Cell>();
                this._cellArray[i,j].SetStatus(
                    this._cellStatusArray[i,j].isAlive,
                    this._cellStatusArray[i,j].isFixed,
                    this._cellStatusArray[i,j].isTarget
                );
            }
        }
    }
}

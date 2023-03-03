using UnityEngine;

namespace GamePlay
{
    public class NeigbourFoundGridTile : MonoBehaviour
    {
     //++++++++++++ NEIGHBOUR FINDING MODULE  +++++++++++++++ 
        // important dont delete 
        [Header("==Neighbour Check Values===")]
        public int left = -1;
        public int right = -1;
        public int up = -1;
        public int bottom = -1;

        public int topRight = -1;
        public int topLeft = -1;
        public int botRight = -1;
        public int botLeft = -1;

        // horizental verticals 
        public GridTile leftNeighbour = null;
        public GridTile rightNeighbour = null;

        public GridTile topNeighbour = null;
        public GridTile bottomNeighbour = null;
        // Diagonal 
        public GridTile bottomLeftNeighbour = null;
        public GridTile bottomRightNeighbour = null;

        public GridTile topLeft_Neighbour = null;
        public GridTile topRight_Neighbour = null;


        public void CheckNeighbour(int countNumberOfBlock, int numOfRows, int numOfCols)
        {

            if ((countNumberOfBlock + 1) % numOfCols != 0) //numofcols=4
                right = countNumberOfBlock + 1;// right 

            if (countNumberOfBlock % numOfCols != 0)
                left = countNumberOfBlock - 1; //left

            if ((countNumberOfBlock - numOfCols) > -1)
                bottom = countNumberOfBlock - numOfCols;  //bottom

            if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)))
            {
                up = countNumberOfBlock + numOfCols;  // top 
            }
            //=======Top Diagonals ==============================================================
            if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)) && (countNumberOfBlock + 1) % numOfCols != 0)
                topRight = countNumberOfBlock + numOfCols + 1;   // top Diagonals right 

            if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)) && (countNumberOfBlock) % numOfCols != 0)
                topLeft = countNumberOfBlock + numOfCols - 1; //  top Diagonals left


            //=========== Bottom Diagonals ==================================================
            if (countNumberOfBlock - numOfCols >= 0 && (countNumberOfBlock + 1) % numOfCols != 0 && (countNumberOfBlock - numOfCols) < ((numOfRows * numOfCols) - 1))
                botRight = (countNumberOfBlock - numOfCols) + 1;   // Bottom  Diagonals right 

            if (countNumberOfBlock - numOfCols >= 0 && (countNumberOfBlock) % numOfCols != 0 && (countNumberOfBlock - numOfCols) < ((numOfRows * numOfCols) - 1))
                botLeft = (countNumberOfBlock - numOfCols) - 1;//  top Diagonals left

            Invoke("SetNeighbour", 1);
        }

        private void SetNeighbour()
        {
            leftNeighbour = GetNeighbour(left);
            rightNeighbour = GetNeighbour(right);

            topNeighbour = GetNeighbour(up);
            bottomNeighbour = GetNeighbour(bottom);
            // Diagonal 
            bottomLeftNeighbour = GetNeighbour(botLeft);
            bottomRightNeighbour = GetNeighbour(botRight);

            topLeft_Neighbour = GetNeighbour(topLeft);
            topRight_Neighbour = GetNeighbour(topRight);
        }

       public GridTile GetNeighbour(int childIndex)
        {
            GameObject parentObject_Grid = this.gameObject.transform.parent.gameObject;
            GridTile NeighbourOf_Block = null;
            if (childIndex >= 0)
            {
                NeighbourOf_Block = parentObject_Grid.gameObject.transform.GetChild(childIndex).gameObject.GetComponent<GridTile>();
            }
            return NeighbourOf_Block;
        }

        

    }
}

﻿using System.Collections.Generic;

namespace DonutMatach
{
    public class Map
    {
        private List<PFCell> pfCells;
        public IList<PFCell> PFCells { get { return pfCells.AsReadOnly(); } }

        public Map(MatchGrid grid) 
        {
            CreateMap(grid);
        }

        private void CreateMap(MatchGrid grid)
        {
            pfCells = new List<PFCell>(grid.Cells.Count);
          //  UnityEngine.Debug.Log("Create new map");

            // create all pfcells
            foreach (var item in grid.Cells)
            {
                PFCell pfc = new PFCell(item);
                pfc.available = (!item.IsDisabled && !item.Blocked && !item.MovementBlocked);
                item.pfCell = pfc;
                pfCells.Add(pfc);
            }

            // set pfcell neighborns
            foreach (var item in pfCells)
            {
                item.CreateNeighBorns();
            }
        }

        public void ResetPath()
        {
            foreach (var item in pfCells)
            {
                item.mather = null;
              //  item.openClose = 0;
              //  item.fCost = 0;
              //  item.gCost = 0;
               // item.hCost = 0;
            }
        }

        public void UpdateMap(MatchGrid grid) // new proc
        {
            // check if grid size 
            int gCount = grid.Cells.Count;
            int pfCount = pfCells.Count;

            if (gCount == pfCount && grid.Cells[0] == pfCells[0].gCell && grid.Cells[gCount-1] == pfCells[pfCount-1].gCell)
            {
               // UnityEngine.Debug.Log("Refresh map");
                foreach (var item in pfCells)
                {
                    item.mather = null;  // item.openClose = 0; item.fCost = 0;item.gCost = 0; item.hCost = 0;
                    item.available = (!item.gCell.IsDisabled && !item.gCell.Blocked);
                }
            }
            else
            {
                CreateMap(grid);
            }
        }

        public PFCell GetRandomPFPositionInMapToGo(PFCell A)
        {
            PFCell B = pfCells[UnityEngine.Random.Range(0, pfCells.Count)];
            while (B == A || B.available)
            {
                B = pfCells[UnityEngine.Random.Range(0, pfCells.Count)];
            }
            return B;
        }

        public override string ToString()
        {
            string res = "";
            foreach (var item in pfCells)
            {
                res += item.ToString();
                res += System.Environment.NewLine;
            }
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    
   public class GameControl
    {
        int _noMines;
        bool _gameLose;
        int _cellUnOpened;
        int _time;
        int _flags;
       public GameControl()
        {
            _noMines = 10;
            _gameLose = false;
            _cellUnOpened = 256;
            _time = 0;
            _flags = 40;
        }
        public int NoMines   // property
        {
            get { return _noMines; }   // get method
            set { _noMines = value; }  // set method
        }
        public bool GameLose   // property
        {
            get { return _gameLose; }   // get method
            set { _gameLose = value; }  // set method
        }
        public int CellUnOpened   // property
        {
            get { return _cellUnOpened; }   // get method
            set { _cellUnOpened = value; }  // set method
        }
        public int Time   // property
        {
            get { return _time; }   // get method
            set { _time = value; }  // set method
        }
        public int Flags// property
        {
            get { return _flags; }   // get method
            set { _flags = value; }  // set method
        }
        public void SetValues(int a,bool b, int c,int d,int e) {
            _noMines = a;
            _gameLose = false;
            _cellUnOpened = c;
            _time = d;
            _flags = e;
        }
    }
    }


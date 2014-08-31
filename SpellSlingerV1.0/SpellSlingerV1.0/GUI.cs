using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    class GUI
    {

        Factory objectFactory;
        ViewPort viewPort;

        public GUI(Factory objectFactory_, ViewPort viewPort_)
        {
            objectFactory = objectFactory_;
            viewPort = viewPort_;
        }

        public void GUIIntro()
        {

        }

        public void GUIMenu()
        {

        }

        public void GUIPlayGame()
        {
            //int padX = 25;
            //int padY = 25;
            //int fill = 10;
            //^Not in demo scope - to be implemented
            //Scaling, individual hotbar buttons, load from spellbook, 50% transparant image (if inactive mark inactive image as active) lulz 

            //For scope of demo this is fine. Spellbook is not currently active  (all objects will be interactive - mouse on arrow will scroll screen etc)
            //Hotbar
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_1, 25, viewPort.ViewPortHeight - 89, 320, 64, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_2, 25, viewPort.ViewPortHeight - 89, 320, 64, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_3, 25, viewPort.ViewPortHeight - 89, 320, 64, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_4, 25, viewPort.ViewPortHeight - 89, 320, 64, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_5, 25, viewPort.ViewPortHeight - 89, 320, 64, false);

            //Spellbook
            objectFactory.CreateGUIComponent(GUI_SPRITES.SPELL_BOOK, viewPort.ViewPortWidth - 89, viewPort.ViewPortHeight - 89, 64, 64, true);

            //Arrows
            int width = 32;
            int height = 32;
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_UP, viewPort.ViewPortWidth / 2 - width/2, 0, width, height, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_DOWN, viewPort.ViewPortWidth / 2 - width / 2, viewPort.ViewPortHeight - height, width, height, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_LEFT, 0, viewPort.ViewPortHeight / 2 - height / 2, width, height, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_RIGHT, viewPort.ViewPortWidth - width, viewPort.ViewPortHeight/2 - height/2, width, height, true);

        }

        public void GUIOptions()
        {

        }

        public void GUIEnd()
        {

        }


    }
}

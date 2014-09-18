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
            //Hotbar - Dimensions of actual .png need to be reworked - not final implication of hotbar graphic
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_1, 130, viewPort.ViewPortHeight - 54, 320, 64, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_2, 130, viewPort.ViewPortHeight - 54, 320, 64, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_3, 130, viewPort.ViewPortHeight - 54, 320, 64, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_4, 130, viewPort.ViewPortHeight - 54, 320, 64, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_5, 130, viewPort.ViewPortHeight - 54, 320, 64, false);

            //Spellbook
            int width = 64;
            int height = 64;
            int pad = 10;
            objectFactory.CreateGUIComponent(GUI_SPRITES.SPELL_BOOK, viewPort.ViewPortWidth - width*0.5f - pad, viewPort.ViewPortHeight - height * 0.5f - pad, 64, 64, true);

            //Arrows
            width = 32;
            height = 32;
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_UP, viewPort.ViewPortWidth * 0.5f, height * 0.5f, width, height, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_DOWN, viewPort.ViewPortWidth * 0.5f, viewPort.ViewPortHeight - height * 0.5f, width, height, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_LEFT, width * 0.5f, viewPort.ViewPortHeight * 0.5f, width, height, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.ARROW_RIGHT, viewPort.ViewPortWidth - width * 0.5f, viewPort.ViewPortHeight * 0.5f, width, height, true);

            //Tower - thank you captain fkn obvious
            width = 32;
            height = 64;
            objectFactory.CreateGUIComponent(GUI_SPRITES.GUI_TOWER, width * 0.5f + pad, height * 0.5f + pad, width, height, true);

        }

        public void GUIOptions()
        {

        }

        public void GUIEnd()
        {

        }


    }
}

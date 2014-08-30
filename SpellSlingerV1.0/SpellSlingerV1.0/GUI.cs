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
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_1, 25, viewPort.ViewPortHeight - 75, 250, 50, true);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_2, 25, viewPort.ViewPortHeight - 75, 250, 50, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_3, 25, viewPort.ViewPortHeight - 75, 250, 50, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_4, 25, viewPort.ViewPortHeight - 75, 250, 50, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_5, 25, viewPort.ViewPortHeight - 75, 250, 50, false);
            objectFactory.CreateGUIComponent(GUI_SPRITES.SPELL_BOOK, viewPort.ViewPortWidth - 64, viewPort.ViewPortHeight - 64, 64, 64, true);
        }

        public void GUIOptions()
        {

        }

        public void GUIEnd()
        {

        }


    }
}

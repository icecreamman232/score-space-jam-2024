using UnityEngine;

namespace JustGame.Script.Manager
{
    public static class LayerManager
    {
        #region Tag

        

        #endregion
        
        #region Layers
        public static int PlayerLayer = 6;
        public static int ObstacleLayer = 7;

        #endregion

        #region Layer Masks
        public static int PlayerMask = 1 << PlayerLayer;
        public static int ObstacleMask = 1 << ObstacleLayer;
        #endregion
        
        
        public static bool IsInLayerMask(int layerWantToCheck, LayerMask layerMask)
        {
            if (((1 << layerWantToCheck) & layerMask) != 0)
            {
                return true;
            }
            return false;
        }
    }

}

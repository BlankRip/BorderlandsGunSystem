using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blank;

namespace Gameplay.Guns
{
    [CreateAssetMenu(fileName = "Gun Spread Config", menuName = "Guns/Gun Spread Config", order = 1)]
    public class GunSpreadConfig : ScriptableObject
    {
        public enum SpreadType { None, Simple, Texture }
        public SpreadType TypeOfSpread;
        public float RecoilRevoverySpeed = 1.0f;
        public float MaxSpreadTime_F = 1.0f;
        
        [Header("Spimple Spread")]
        [SerializeField] Vector3 spreadAmount;

        [Header("Texture-Based Spread")]
        [Range(0.001f, 5.0f)]
        [SerializeField] float spreadMultiplier = 0.1f;
        [SerializeField] Texture2D spreadTexture;

        public Vector3 GetSpread(float _shootTime)
        {
            Vector3 spreadAmount = Vector3.zero;
            switch (TypeOfSpread)
            {
                case SpreadType.Simple:
                {
                        Vector3 targetSpread = new Vector3(
                        Random.Range(this.spreadAmount.x, this.spreadAmount.x),
                        Random.Range(this.spreadAmount.y, this.spreadAmount.y),
                        Random.Range(this.spreadAmount.z, this.spreadAmount.z)
                    );
                    spreadAmount = Vector3.Lerp(Vector3.zero, targetSpread, Mathf.Clamp01(_shootTime/MaxSpreadTime_F));
                    break;
                }
                case SpreadType.Texture:
                {
                    spreadAmount = GetTextureDirection(_shootTime);
                    spreadAmount *= spreadMultiplier;
                    break;
                }
            }
            return spreadAmount;
        }

        private Vector3 GetTextureDirection(float _shootTime)
        {
            //Create the amount of area we sample the texture for
            Vector2 halfSize = new Vector2(spreadTexture.width/2, spreadTexture.height/2);
            int halfSquareExtents = Mathf.CeilToInt(
                Mathf.Lerp(1, halfSize.x, Mathf.Clamp01(_shootTime/MaxSpreadTime_F))
                );
            int minX = Mathf.FloorToInt(halfSize.x) - halfSquareExtents;
            int minY = Mathf.FloorToInt(halfSize.y) - halfSquareExtents;

            //Sampling the texutre using the square
            int squareExtents = halfSquareExtents * 2;
            Color[] sampledColors = spreadTexture.GetPixels(minX, minY, squareExtents, squareExtents);
            float[] colorsAsGrey = System.Array.ConvertAll(sampledColors, (color) => color.grayscale);

            //Selecting a pixel that is white
            float totalGreyValue = colorsAsGrey.Sum();
            float grey = Random.Range(0, totalGreyValue);
            int i = 0;
            for (; i < colorsAsGrey.Length; i++)
            {
                grey -= colorsAsGrey[i];
                if(grey <= 0)
                    break;
            }

            //Get index on texture for the pixel selected
            int x = minX + i % (squareExtents);
            int y = minY + i / (squareExtents);
            Vector2 targetPosition = new Vector2(x, y);

            Vector2 direction = (targetPosition - halfSize) / halfSize.x;
            return direction;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficSystem
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] spritesUp;
        [SerializeField] private Sprite[] spritesDown;

        int counter = 1;

        private GameObject[] path = { };
        private Sprite[] sprites = { };

        float startingSpeed;

        bool down;

        private void Start()
        {
            startingSpeed = speed;

            System.Random rnd = new System.Random();
            int r = rnd.Next(0, 2);

            if (r == 1)
            {
                path = TrafficManager.Instance.fPath;
                sprites = spritesUp;
            }
            else
            {
                path = TrafficManager.Instance.sPath;
                sprites = spritesDown;
                down = true;
            }

            SetRandomSprite();

            int p = rnd.Next(0, path.Length - 1);
            counter = p;
            transform.position = (Vector2)path[p].transform.position;

            if (p % 2 == 0 && p != 0)
                spriteRenderer.flipX = true;


            if (counter == 0)
                spriteRenderer.flipX = true;

            if (down)
                spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        private void Update()
        {
            if (path.Length > 0)
            {
                if (counter < path.Length && transform != null && path[counter] != null && (Vector2)transform.position != (Vector2)path[counter].transform.position)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, path[counter].transform.position, step);
                }
                else
                {
                    if (counter < path.Length)
                    {
                        counter++;
                        spriteRenderer.flipX = !spriteRenderer.flipX;
                    }
                    else
                    {
                        counter = 0;
                        if (path[0] != null)
                        {
                            transform.position = (Vector2)path[0].transform.position;
                            SetRandomSprite();

                            spriteRenderer.flipX = !spriteRenderer.flipX;

                        }
                    }
                }
            }
        }

        private void SetRandomSprite()
        {
            System.Random rnd = new System.Random();

            int s = rnd.Next(0, sprites.Length);

            spriteRenderer.sprite = sprites[s];

            if (s == 0)
                speed = startingSpeed - 0.3f;
            else if (s == 6)
                speed = 1.1f;
            else
                speed = Random.Range(startingSpeed - 0.1f, startingSpeed + 0.1f);
        }
    }
}
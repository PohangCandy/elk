using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

        Transform player;

        public float VisibleRange { get; set; }

        public float AttackRange { get; set; }

        public float Hp { get; set; }

        Renderer rend;
            
        State state;
        
        public void ChangeColor(Color newColor)
        {
                rend.material.SetColor("_Color", newColor);
        }

        
        void Start()
        {
                AttackRange = 5f;
                VisibleRange = 10f;
                Hp = 18.0f;

                rend = GetComponent<Renderer>();
                
                player = GameObject.Find("Player").transform;
                state = new StrollState(this, player);
                state.Enter();
        }


        private void Update()
        {
                State state_ = state.HandleInput();
                if (state_ != null)
                {
                    state.Exit();
                    state = state_;
                    state.Enter();
                }

                state.DoAction();
        }
}


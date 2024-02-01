using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

		void Start()
		{
			open = false;
		}

        private void OnTriggerEnter(Collider other)
        {
			if (other.CompareTag("Player"))
            {
				
				
				if (open == false)
				{
						
					StartCoroutine(opening());
							
				}
				
			}

		

		}

		IEnumerator opening()
		{
			openandclose.Play("Opening");
			open = true;
            yield return new WaitForSeconds(.5f);
        }



	}
}
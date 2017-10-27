using System;
using UnityEngine;
using UnityEditor;

namespace Scripts.Game.UI
{
	public class HpBarViewModel {
		/// <summary>
		/// The minimum value the HP bar state can have
		/// </summary>
		/// <value>The minimum value.</value>
		public float MinValue { get; set; }
		public float MaxValue { get; set; }
		private float currentValue;
		public float Value { 
			get { return this.currentValue;} 
			set { 
				if (value > this.MaxValue) {
					this.currentValue = this.MaxValue;
				} else if (value < this.MinValue) {
					this.currentValue = this.MinValue;
				} else {
					this.currentValue = value;
				}
				this.HpBarUi.UpdateBar (this.currentValue);
			}
		}

		private HpBarUi HpBarUi { get; set;}

		public HpBarViewModel(HpBarUi bar) {
			this.HpBarUi = bar;
			this.MinValue = 0;
			this.MaxValue = 0;
			this.Value = 0;
		}

		public void MoveBar(Rect newRect) {
			this.HpBarUi.borderRect = newRect;
		}

	}


	/// <summary>
	/// Represents a <b>Unity object</b> allowing you to show an Health bar.
	/// Actually this bar can be used to other things aside shown HP (it can be used to show MP or some other stuff)
	/// </summary>
	[ExecuteInEditMode]
	public class HpBarUi : MonoBehaviour
	{
		public HpBarViewModel HpBarViewModel { get; set;}
		[Tooltip("a rectangle specifiying the dimension, in pixel, of the health bar. The rectangle is the space the bar actually comsumes")]
		public Rect borderRect = new Rect(0, 0, 100, 15);
		[Tooltip("how thick the border is, in pixel.")]
		public int borderThickness = 5;
		[Tooltip("if true, we will show the hpbar inside the \"Game View\" of Unity even in Edit mode")]
		public bool activateInEditMode = false;


		private Rect HealthRect { get; set;}
		private Rect BorderRect { get { return this.borderRect; } set { this.borderRect = value; } }
		private int BorderThickness { get { return borderThickness; } set { this.borderThickness = value; } }
		private bool ActivateInEditMode { get { return activateInEditMode; } set { this.activateInEditMode = value; }}

		public HpBarUi() {
			this.HpBarViewModel = new HpBarViewModel (this);
		}

		public void Start() {
			this.BorderRect = new Rect (0, 0, this.BorderRect.width, this.BorderRect.height);
			this.HealthRect = new Rect (0 + this.BorderThickness, 0 + this.BorderThickness, this.BorderRect.width - 2 * this.BorderThickness, this.BorderRect.height - 2 * this.BorderThickness);
		}

		public void OnGUI() {
			if (Application.isPlaying || activateInEditMode) {
				EditorGUI.DrawRect (this.BorderRect, Color.black);
				EditorGUI.DrawRect (this.HealthRect, Color.red);
			}
		}

		/// <summary>
		/// Visually updates the bar
		/// </summary>
		/// <param name="newValue">A value between 0 and 1, representing how much we need to fill the bar</param>
		internal void UpdateBar(float newValue) {
			this.HealthRect = new Rect (
				this.HealthRect.x,
				this.HealthRect.y,
				newValue * (this.BorderRect.width - 2 * this.BorderThickness),
				this.HealthRect.height
			);
		}
	}
}


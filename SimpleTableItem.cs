namespace UIWidgets.Examples
{
	using System;
	using UIWidgets;
	using UnityEngine;

	/// <summary>
	/// SimpleTable item.
	/// </summary>
	[Serializable]
	public class SimpleTableItem
	{
		/// <summary>
		/// Field1.
		/// </summary>
		[SerializeField]
		public string Field1;

		/// <summary>
		/// Field2.
		/// </summary>
		[SerializeField]
		public string Field2;

		/// <summary>
		/// Field3.
		/// </summary>
		[SerializeField]
		public string Field3;

		/// <summary>
		/// Field4.
		/// </summary>
		[SerializeField]
		public string Field4;

		/// <summary>
		/// Field5.
		/// </summary>
		[SerializeField]
		public string Field5;

		/// <summary>
		/// Field6.
		/// </summary>
		[SerializeField]
		public string Field6;

		/// <summary>
		/// Field7.
		/// </summary>
		[SerializeField]
		public string Field7;

		/// <summary>
		/// Field8.
		/// </summary>
		[SerializeField]
		public string Field8;

		/// <summary>
		/// Convert instance to string.
		/// </summary>
		/// <returns>String.</returns>
		public override string ToString()
		{
			return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8);
		}
	}
}
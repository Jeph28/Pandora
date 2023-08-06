namespace UIWidgets.Examples
{
	using System;
	using UIWidgets;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// SimpleTable component.
	/// </summary>
	public class SimpleTableComponent : ListViewItem, IViewData<SimpleTableItem>, IUpgradeable
	{
		/// <summary>
		/// Field1.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field1Adapter.")]
		public Text Field1;

		/// <summary>
		/// Field2.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field2Adapter.")]
		public Text 
		Field2;

		/// <summary>
		/// Field3.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field3Adapter.")]
		public Text Field3;

		/// <summary>
		/// Field4.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field4Adapter.")]
		public Text Field4;

		/// <summary>
		/// Field5.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field4Adapter.")]
		public Text Field5;

		/// <summary>
		/// Field6.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field4Adapter.")]
		public Text Field6;

		/// <summary>
		/// Field7.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field4Adapter.")]
		public Text Field7;

		/// <summary>
		/// Field8.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with Field4Adapter.")]
		public Text Field8;

		/// <summary>
		/// Field1.
		/// </summary>
		[SerializeField]
		public TextAdapter Field1Adapter;

		/// <summary>
		/// Field2.
		/// </summary>
		[SerializeField]
		public TextAdapter Field2Adapter;

		/// <summary>
		/// Field3.
		/// </summary>
		[SerializeField]
		public TextAdapter Field3Adapter;

		/// <summary>
		/// Field4.
		/// </summary>
		[SerializeField]
		public TextAdapter Field4Adapter;

		/// <summary>
		/// Field5.
		/// </summary>
		[SerializeField]
		public TextAdapter Field5Adapter;

		/// <summary>
		/// Field6.
		/// </summary>
		[SerializeField]
		public TextAdapter Field6Adapter;

		/// <summary>
		/// Field7.
		/// </summary>
		[SerializeField]
		public TextAdapter Field7Adapter;

		/// <summary>
		/// Field8.
		/// </summary>
		[SerializeField]
		public TextAdapter Field8Adapter;

		/// <summary>
		/// Init graphics foreground.
		/// </summary>
		protected override void GraphicsForegroundInit()
		{
			if (GraphicsForegroundVersion == 0)
			{
				
				Foreground = new Graphic[]
				{
					UtilitiesUI.GetGraphic(Field1Adapter),
					UtilitiesUI.GetGraphic(Field2Adapter),
					UtilitiesUI.GetGraphic(Field3Adapter),
					UtilitiesUI.GetGraphic(Field4Adapter),
					UtilitiesUI.GetGraphic(Field5Adapter),
					UtilitiesUI.GetGraphic(Field6Adapter),
					UtilitiesUI.GetGraphic(Field7Adapter),
					UtilitiesUI.GetGraphic(Field8Adapter),
				};
				GraphicsForegroundVersion = 1;
			}
		}

		/// <summary>
		/// Init graphics background.
		/// </summary>
		protected override void GraphicsBackgroundInit()
		{
			if (GraphicsBackgroundVersion == 0)
			{
				graphicsBackground = Compatibility.EmptyArray<Graphic>();
				GraphicsBackgroundVersion = 1;
			}
		}

		/// <summary>
		/// Gets the objects to resize.
		/// </summary>
		/// <value>The objects to resize.</value>
		public GameObject[] ObjectsToResize
		{
			get
			{
				return new[]
				{
					Field1Adapter.transform.parent.gameObject,
					Field2Adapter.transform.parent.gameObject,
					Field3Adapter.transform.parent.gameObject,
					Field4Adapter.transform.parent.gameObject,
					Field5Adapter.transform.parent.gameObject,
					Field6Adapter.transform.parent.gameObject,
					Field7Adapter.transform.parent.gameObject,
					Field8Adapter.transform.parent.gameObject,
				};
			}
		}

		/// <summary>
		/// Set data.
		/// </summary>
		/// <param name="item">Item.</param>
		public void SetData(SimpleTableItem item)
		{
			Field1Adapter.text = item.Field1;
			Field2Adapter.text = item.Field2;
			Field3Adapter.text = item.Field3;
			Field4Adapter.text = item.Field4;
			Field5Adapter.text = item.Field5;
			Field6Adapter.text = item.Field6;
			Field7Adapter.text = item.Field7;
			Field8Adapter.text = item.Field8;
		}

		/// <summary>
		/// Upgrade this instance.
		/// </summary>
		public override void Upgrade()
		{
#pragma warning disable 0612, 0618
			Utilities.GetOrAddComponent(Field1, ref Field1Adapter);
			Utilities.GetOrAddComponent(Field2, ref Field2Adapter);
			Utilities.GetOrAddComponent(Field3, ref Field3Adapter);
			Utilities.GetOrAddComponent(Field4, ref Field4Adapter);
			Utilities.GetOrAddComponent(Field5, ref Field5Adapter);
			Utilities.GetOrAddComponent(Field6, ref Field6Adapter);
			Utilities.GetOrAddComponent(Field7, ref Field7Adapter);
			Utilities.GetOrAddComponent(Field8, ref Field8Adapter);
#pragma warning restore 0612, 0618
		}
	}
}
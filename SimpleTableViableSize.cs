namespace UIWidgets.Examples
{
	using UnityEngine;

	/// <summary>
	/// Test SimpleTable.
	/// </summary>
	public class SimpleTableViableSize : MonoBehaviour
	{
		/// <summary>
		/// SimpleTable.
		/// </summary>
		public SimpleTable Table;
		public DryerMachine dryerMachine;
		string MoreLess = "\u00B1";

		/// <summary>
		/// Add item.
		/// </summary>

		void Start()
		{
			Cursor.lockState = CursorLockMode.None;
        	Cursor.visible = true;
			dryerMachine.BatchSize(GameManager.previousUnpackPastaScore , GameManager.UnpackPastaScore);
			dryerMachine.ResultTable();
			Add();
		}

		public void Add()
		{
			for (int i = 1; i <= GameManager.Batch; i++)
			{
				Table.DataSource.Add(new SimpleTableItem() { Field1 = i.ToString(), Field2 = GameManager.batchSizeList[i-1].ToString(), Field3 = GameManager.pastaHumidityList[i-1].ToString("F2") + " " + MoreLess + " " + GameManager.StdDevHumidity.ToString("F2"), Field4 = GameManager.pastaColorList[i-1], Field5 = GameManager.pastaCrakingList[i-1], Field6 = GameManager.pastaMicroorganismsList[i-1], Field7 = GameManager.pastaWeightList[i-1].ToString("F2") + " " + MoreLess + " " + GameManager.pastaStdDevWeightList[i-1].ToString("F2") , Field8 = GameManager.resultTable[i-1]});
			}
		}

		/// <summary>
		/// Remove item.
		/// </summary>
		public void Remove()
		{
			Table.DataSource.RemoveAt(0);
		}

		/// <summary>
		/// Add item at start.
		/// </summary>
		public void AddAtStart()
		{
			var item = new SimpleTableItem() { Field1 = "First row 1", Field2 = "First row 2", Field3 = "First row 3" };
			Table.DataSource.Insert(0, item);
		}
	}
}
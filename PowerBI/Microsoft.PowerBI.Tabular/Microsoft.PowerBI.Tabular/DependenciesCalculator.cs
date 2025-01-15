using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000F7 RID: 247
	[Guid("786606A9-D908-42e9-95A5-8F4DDB6E8BA1")]
	internal sealed class DependenciesCalculator
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x0007831C File Offset: 0x0007651C
		public static IMajorObject[] OrderObjects(IMajorObject[] objects)
		{
			if (objects == null)
			{
				throw new ArgumentNullException("objects");
			}
			int num = objects.Length;
			if (num == 0)
			{
				return new IMajorObject[0];
			}
			DependenciesCalculator.Node[] array = new DependenciesCalculator.Node[num];
			int[] array2 = new int[num];
			array[0] = null;
			array2[0] = 0;
			for (int i = 1; i < num; i++)
			{
				array[i] = null;
				array2[i] = 0;
				if (objects[i] != null)
				{
					for (int j = 0; j < i; j++)
					{
						if (objects[j] != null)
						{
							if (objects[i].DependsOn(objects[j]))
							{
								array[i] = new DependenciesCalculator.Node(j, array[i]);
								array2[j]++;
							}
							if (objects[j].DependsOn(objects[i]))
							{
								array[j] = new DependenciesCalculator.Node(i, array[j]);
								array2[i]++;
							}
						}
					}
				}
			}
			int num2 = num - 1;
			IMajorObject[] array3 = new IMajorObject[num];
			int num3 = -1;
			bool flag = false;
			while (!flag && num2 >= 0)
			{
				flag = true;
				if (num3 == -1)
				{
					for (int k = 0; k < num; k++)
					{
						if (array2[k] == 0)
						{
							num3 = k;
							break;
						}
					}
				}
				if (num3 != -1)
				{
					int num4 = num3;
					num3 = -1;
					flag = false;
					array2[num4] = -1;
					array3[num2] = objects[num4];
					num2--;
					for (DependenciesCalculator.Node node = array[num4]; node != null; node = node.Next)
					{
						array2[node.Value]--;
					}
				}
			}
			if (num2 != -1)
			{
				throw new InvalidOperationException(SR.OrderObjects_CircuitFound);
			}
			return array3;
		}

		// Token: 0x020002F8 RID: 760
		private class Node
		{
			// Token: 0x060023D3 RID: 9171 RVA: 0x000E2DC3 File Offset: 0x000E0FC3
			public Node(int value, DependenciesCalculator.Node next)
			{
				this.Value = value;
				this.Next = next;
			}

			// Token: 0x04000AE6 RID: 2790
			public int Value;

			// Token: 0x04000AE7 RID: 2791
			public DependenciesCalculator.Node Next;
		}
	}
}

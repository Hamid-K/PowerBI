using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200024C RID: 588
	internal static class StaticRoots
	{
		// Token: 0x060013B4 RID: 5044 RVA: 0x0003DB9C File Offset: 0x0003BD9C
		internal static void Initialize(object[] objectCollection)
		{
			int num = 1 + (objectCollection.Length - 1) / 16;
			int num2 = objectCollection.Length - 16 * (num - 1);
			lock (StaticRoots._lockObject)
			{
				int num3 = 0;
				for (int i = 0; i < 16; i++)
				{
					int num4 = ((i < num2) ? num : (num - 1));
					object obj;
					if (num4 == 0)
					{
						obj = null;
					}
					else if (num4 == 1)
					{
						obj = objectCollection[num3];
					}
					else
					{
						object[] array = new object[num4];
						for (int j = 0; j < num4; j++)
						{
							array[j] = objectCollection[num3 + j];
						}
						obj = array;
					}
					StaticRoots.SetReference(i, obj);
					num3 += num4;
				}
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0003DC58 File Offset: 0x0003BE58
		private static void SetReference(int index, object value)
		{
			switch (index)
			{
			case 0:
				StaticRoots._staticReference0 = value;
				return;
			case 1:
				StaticRoots._staticReference1 = value;
				return;
			case 2:
				StaticRoots._staticReference2 = value;
				return;
			case 3:
				StaticRoots._staticReference3 = value;
				return;
			case 4:
				StaticRoots._staticReference4 = value;
				return;
			case 5:
				StaticRoots._staticReference5 = value;
				return;
			case 6:
				StaticRoots._staticReference6 = value;
				return;
			case 7:
				StaticRoots._staticReference7 = value;
				return;
			case 8:
				StaticRoots._staticReference8 = value;
				return;
			case 9:
				StaticRoots._staticReference9 = value;
				return;
			case 10:
				StaticRoots._staticReference10 = value;
				return;
			case 11:
				StaticRoots._staticReference11 = value;
				return;
			case 12:
				StaticRoots._staticReference12 = value;
				return;
			case 13:
				StaticRoots._staticReference13 = value;
				return;
			case 14:
				StaticRoots._staticReference14 = value;
				return;
			case 15:
				StaticRoots._staticReference15 = value;
				return;
			default:
				return;
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0003DD20 File Offset: 0x0003BF20
		internal static object[] GetReferences()
		{
			return new object[]
			{
				StaticRoots._staticReference0,
				StaticRoots._staticReference1,
				StaticRoots._staticReference2,
				StaticRoots._staticReference3,
				StaticRoots._staticReference4,
				StaticRoots._staticReference5,
				StaticRoots._staticReference6,
				StaticRoots._staticReference7,
				StaticRoots._staticReference8,
				StaticRoots._staticReference9,
				StaticRoots._staticReference10,
				StaticRoots._staticReference11,
				StaticRoots._staticReference12,
				StaticRoots._staticReference13,
				StaticRoots._staticReference14,
				StaticRoots._staticReference15
			};
		}

		// Token: 0x04000BD1 RID: 3025
		internal const int NumberOfReferences = 16;

		// Token: 0x04000BD2 RID: 3026
		private static object _staticReference0;

		// Token: 0x04000BD3 RID: 3027
		private static object _staticReference1;

		// Token: 0x04000BD4 RID: 3028
		private static object _staticReference2;

		// Token: 0x04000BD5 RID: 3029
		private static object _staticReference3;

		// Token: 0x04000BD6 RID: 3030
		private static object _staticReference4;

		// Token: 0x04000BD7 RID: 3031
		private static object _staticReference5;

		// Token: 0x04000BD8 RID: 3032
		private static object _staticReference6;

		// Token: 0x04000BD9 RID: 3033
		private static object _staticReference7;

		// Token: 0x04000BDA RID: 3034
		private static object _staticReference8;

		// Token: 0x04000BDB RID: 3035
		private static object _staticReference9;

		// Token: 0x04000BDC RID: 3036
		private static object _staticReference10;

		// Token: 0x04000BDD RID: 3037
		private static object _staticReference11;

		// Token: 0x04000BDE RID: 3038
		private static object _staticReference12;

		// Token: 0x04000BDF RID: 3039
		private static object _staticReference13;

		// Token: 0x04000BE0 RID: 3040
		private static object _staticReference14;

		// Token: 0x04000BE1 RID: 3041
		private static object _staticReference15;

		// Token: 0x04000BE2 RID: 3042
		private static object _lockObject = new object();
	}
}

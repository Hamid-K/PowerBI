using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200004C RID: 76
	public abstract class MdDataset : IMDDataset, IColumnsInfo, IAccessor, IDBAsynchStatus
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000277 RID: 631
		[global::System.Runtime.CompilerServices.Nullable(1)]
		protected abstract Axis[] Axes
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000278 RID: 632
		[global::System.Runtime.CompilerServices.Nullable(1)]
		protected abstract ICells Cells
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000083B6 File Offset: 0x000065B6
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public IColumnsInfo ColumnsInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.Cells.ColumnsInfo;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000083C3 File Offset: 0x000065C3
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public IAccessor Accessor
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.Cells.Accessor;
			}
		}

		// Token: 0x0600027B RID: 635
		protected unsafe abstract int GetAxisRowsetInternal(IntPtr unkOuter, DBCOUNTITEM axisIndex, ref Guid riid, uint propertySetsCount, DBPROPSET* propertySets, out IntPtr rowset);

		// Token: 0x0600027C RID: 636 RVA: 0x000083D0 File Offset: 0x000065D0
		unsafe void IMDDataset.FreeAxisInfo(DBCOUNTITEM axesCount, MDAXISINFO* axesInfo)
		{
			if (axesInfo != null)
			{
				Marshal.FreeCoTaskMem((IntPtr)((void*)axesInfo));
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000083E4 File Offset: 0x000065E4
		unsafe void IMDDataset.GetAxisInfo(out DBCOUNTITEM axesCount, out MDAXISINFO* nativeAxisInfos)
		{
			axesCount = new DBCOUNTITEM
			{
				Value = 0UL
			};
			nativeAxisInfos = (IntPtr)((UIntPtr)0);
			if (this.Axes == null || this.Axes.Length == 0)
			{
				return;
			}
			int num = this.Axes.Length * sizeof(MDAXISINFO);
			int num3;
			int num4;
			int nativeBufferSizeForDimensionNames;
			int num5;
			checked
			{
				int num2 = this.Axes.Aggregate(0, (int sum, Axis axis) => sum + axis.Dimensions.Length);
				num3 = num2 * sizeof(DBORDINAL);
				num4 = num2 * sizeof(char*);
				nativeBufferSizeForDimensionNames = this.GetNativeBufferSizeForDimensionNames();
				num5 = num + num3 + num4 + nativeBufferSizeForDimensionNames;
			}
			using (ComHeap comHeap = new ComHeap())
			{
				byte* ptr = (byte*)comHeap.Alloc(num5);
				MDAXISINFO* ptr2 = (MDAXISINFO*)ptr;
				DBORDINAL* ptr3 = (DBORDINAL*)(ptr + num);
				char** ptr4 = (char**)(ptr + (num + num3));
				char* ptr5 = (char*)(ptr + (num + num3 + num4));
				int num6 = nativeBufferSizeForDimensionNames;
				for (int i = 0; i < this.Axes.Length; i++)
				{
					Axis axis2 = this.Axes[i];
					uint num7 = (uint)axis2.Dimensions.Length;
					ptr2->Size = new DBLENGTH
					{
						Value = (ulong)sizeof(MDAXISINFO)
					};
					ptr2->AxisIndex = new DBCOUNTITEM
					{
						Value = (ulong)axis2.Index
					};
					ptr2->DimensionsCount = new DBCOUNTITEM
					{
						Value = (ulong)num7
					};
					ptr2->CoordinatesCount = new DBCOUNTITEM
					{
						Value = (ulong)axis2.CoordinatesCount
					};
					ptr2->DimensionColumnCounts = ptr3;
					ptr2->DimensionNames = ptr4;
					int num8 = 0;
					while ((long)num8 < (long)((ulong)num7))
					{
						ptr3->Value = (ulong)axis2.Dimensions[num8].ColumnsCount;
						*(IntPtr*)ptr4 = this.CopyStringToNativeByteBuffer(ref ptr5, ref num6, axis2.Dimensions[num8].Name);
						ptr3++;
						ptr4 += sizeof(char*) / sizeof(char*);
						num8++;
					}
					ptr2++;
				}
				comHeap.Commit();
				axesCount = new DBCOUNTITEM
				{
					Value = (ulong)this.Axes.Length
				};
				nativeAxisInfos = ptr;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000862C File Offset: 0x0000682C
		unsafe int IMDDataset.GetAxisRowset(IntPtr unkOuter, DBCOUNTITEM axisIndex, ref Guid riid, uint propertySetsCount, DBPROPSET* propertySets, out IntPtr rowset)
		{
			return this.GetAxisRowsetInternal(unkOuter, axisIndex, ref riid, propertySetsCount, propertySets, out rowset);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000863D File Offset: 0x0000683D
		unsafe int IMDDataset.GetCellData(HACCESSOR accessor, DBORDINAL startCell, DBORDINAL endCell, void* data)
		{
			return this.Cells.GetCells(accessor, startCell, endCell, data);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000864F File Offset: 0x0000684F
		int IMDDataset.GetSpecification(ref Guid riid, out IntPtr specification)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008656 File Offset: 0x00006856
		unsafe void IColumnsInfo.GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings)
		{
			this.ColumnsInfo.GetColumnInfo(out countColumnInfos, out nativeColumnInfos, out nativeStrings);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008666 File Offset: 0x00006866
		unsafe void IColumnsInfo.MapColumnIDs(DBORDINAL columnIDCount, DBID* columnIDs, DBORDINAL* columns)
		{
			this.ColumnsInfo.MapColumnIDs(columnIDCount, columnIDs, columns);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008676 File Offset: 0x00006876
		unsafe void IAccessor.AddRefAccessor(HACCESSOR accessor, uint* refCount)
		{
			this.Accessor.AddRefAccessor(accessor, refCount);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008685 File Offset: 0x00006885
		unsafe void IAccessor.CreateAccessor(DBACCESSORFLAGS accessorFlags, DBCOUNTITEM bindingCount, DBBINDING* bindings, DBLENGTH rowSize, out HACCESSOR accessor, DBBINDSTATUS* status)
		{
			this.Accessor.CreateAccessor(accessorFlags, bindingCount, bindings, rowSize, out accessor, status);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000869B File Offset: 0x0000689B
		unsafe void IAccessor.GetBindings(HACCESSOR accessor, out DBACCESSORFLAGS accessorFlags, out DBCOUNTITEM bindingCount, out DBBINDING* bindings)
		{
			this.Accessor.GetBindings(accessor, out accessorFlags, out bindingCount, out bindings);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000086AD File Offset: 0x000068AD
		unsafe void IAccessor.ReleaseAccessor(HACCESSOR accessor, uint* refCount)
		{
			this.Accessor.ReleaseAccessor(accessor, refCount);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000086BC File Offset: 0x000068BC
		void IDBAsynchStatus.Abort(HCHAPTER chapter, DBASYNCHOP operation)
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000086BE File Offset: 0x000068BE
		unsafe void IDBAsynchStatus.GetStatus(HCHAPTER chapter, DBASYNCHOP operation, DBCOUNTITEM* progress, DBCOUNTITEM* progressMax, out DBASYNCHPHASE asynchPhase, char** statusText)
		{
			if (progress != null)
			{
				*progress = default(DBCOUNTITEM);
			}
			if (progressMax != null)
			{
				*progressMax = default(DBCOUNTITEM);
			}
			if (statusText != null)
			{
				*(IntPtr*)statusText = (IntPtr)((UIntPtr)0);
			}
			asynchPhase = DBASYNCHPHASE.COMPLETE;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000086EC File Offset: 0x000068EC
		private int GetNativeBufferSizeForDimensionNames()
		{
			int num = 0;
			Axis[] axes = this.Axes;
			for (int i = 0; i < axes.Length; i++)
			{
				checked
				{
					foreach (Dimension dimension in axes[i].Dimensions)
					{
						num += (dimension.Name.Length + 1) * 2;
					}
				}
			}
			return num;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008748 File Offset: 0x00006948
		private unsafe char* CopyStringToNativeByteBuffer(ref char* buffer, ref int remainingSizeInCharacters, [global::System.Runtime.CompilerServices.Nullable(1)] string source)
		{
			if (source == null)
			{
				source = string.Empty;
			}
			int num = source.Length + 1;
			RuntimeChecks.Check(num <= remainingSizeInCharacters, "Insufficient buffer");
			char* ptr = buffer;
			for (int i = 0; i < source.Length; i++)
			{
				ptr[i] = source[i];
			}
			ptr[source.Length] = '\0';
			remainingSizeInCharacters -= num;
			buffer += (IntPtr)num * 2;
			return ptr;
		}
	}
}

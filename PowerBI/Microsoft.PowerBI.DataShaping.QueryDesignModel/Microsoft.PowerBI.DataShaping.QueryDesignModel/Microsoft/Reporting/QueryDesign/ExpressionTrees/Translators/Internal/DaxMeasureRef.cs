using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200013B RID: 315
	internal readonly struct DaxMeasureRef : IEquatable<DaxMeasureRef>
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x0003051E File Offset: 0x0002E71E
		internal DaxMeasureRef(string measureName, DaxTableRef tableRef)
		{
			this.MeasureName = measureName;
			this.TableRef = tableRef;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0003052E File Offset: 0x0002E72E
		public string MeasureName { get; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00030538 File Offset: 0x0002E738
		public string TableName
		{
			get
			{
				return this.TableRef.TableName;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00030553 File Offset: 0x0002E753
		public DaxTableRef TableRef { get; }

		// Token: 0x0600114C RID: 4428 RVA: 0x0003055C File Offset: 0x0002E75C
		public override string ToString()
		{
			return new DaxColumnRef(this.MeasureName, this.TableRef).ToString();
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00030588 File Offset: 0x0002E788
		public override bool Equals(object obj)
		{
			DaxMeasureRef? daxMeasureRef = obj as DaxMeasureRef?;
			return daxMeasureRef != null && this.Equals(daxMeasureRef.Value);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000305BC File Offset: 0x0002E7BC
		public override int GetHashCode()
		{
			return DaxRef.NameComparer.GetHashCode(this.MeasureName) ^ this.TableRef.GetHashCode();
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000305F0 File Offset: 0x0002E7F0
		public bool Equals(DaxMeasureRef other)
		{
			return DaxRef.NameComparer.Equals(this.MeasureName, other.MeasureName) && this.TableRef.Equals(other.TableRef);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003062D File Offset: 0x0002E82D
		public static bool operator ==(DaxMeasureRef left, DaxMeasureRef right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00030637 File Offset: 0x0002E837
		public static bool operator !=(DaxMeasureRef left, DaxMeasureRef right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000AC6 RID: 2758
		public static readonly DaxMeasureRef Empty;
	}
}

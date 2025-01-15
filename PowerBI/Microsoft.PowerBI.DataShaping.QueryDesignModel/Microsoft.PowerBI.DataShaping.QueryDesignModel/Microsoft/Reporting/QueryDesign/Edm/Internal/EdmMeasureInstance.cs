using System;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200020B RID: 523
	public struct EdmMeasureInstance : IEquatable<EdmMeasureInstance>, ICheckable
	{
		// Token: 0x06001877 RID: 6263 RVA: 0x00043192 File Offset: 0x00041392
		internal EdmMeasureInstance(EntitySet entity, EdmMeasure measure)
		{
			this._measure = ArgumentValidation.CheckNotNull<EdmMeasure>(measure, "measure");
			this._entity = ArgumentValidation.CheckNotNull<EntitySet>(entity, "entity");
			ArgumentValidation.CheckCondition(entity.ElementType == measure.DeclaringType, "measure");
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x000431D0 File Offset: 0x000413D0
		public static EdmMeasureInstance Empty
		{
			get
			{
				return default(EdmMeasureInstance);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x000431E6 File Offset: 0x000413E6
		public bool IsValid
		{
			get
			{
				return this.Measure != null;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x000431F1 File Offset: 0x000413F1
		internal EdmMeasure Measure
		{
			get
			{
				return this._measure;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x000431F9 File Offset: 0x000413F9
		internal EntitySet Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00043204 File Offset: 0x00041404
		public QualifiedName Path
		{
			get
			{
				return this.QualifiedName;
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00043224 File Offset: 0x00041424
		public static implicit operator EdmPropertyInstance(EdmMeasureInstance measureInstance)
		{
			if (measureInstance == EdmMeasureInstance.Empty)
			{
				return EdmPropertyInstance.Empty;
			}
			return measureInstance.Entity.PropertyInstance(measureInstance.Measure);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0004324C File Offset: 0x0004144C
		public static bool operator ==(EdmMeasureInstance left, EdmMeasureInstance right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00043256 File Offset: 0x00041456
		public static bool operator !=(EdmMeasureInstance left, EdmMeasureInstance right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00043264 File Offset: 0x00041464
		public override bool Equals(object value)
		{
			EdmMeasureInstance? edmMeasureInstance = value as EdmMeasureInstance?;
			return edmMeasureInstance != null && this.Equals(edmMeasureInstance.Value);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00043295 File Offset: 0x00041495
		public override int GetHashCode()
		{
			if (this.Measure == null && this.Entity == null)
			{
				return base.GetType().GetHashCode();
			}
			return this.Measure.GetHashCode() ^ this.Entity.GetHashCode();
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000432D4 File Offset: 0x000414D4
		public bool Equals(EdmMeasureInstance other)
		{
			return object.Equals(this.Measure, other.Measure) && object.Equals(this.Entity, other.Entity);
		}

		// Token: 0x04000D0D RID: 3341
		private readonly EdmMeasure _measure;

		// Token: 0x04000D0E RID: 3342
		private readonly EntitySet _entity;
	}
}

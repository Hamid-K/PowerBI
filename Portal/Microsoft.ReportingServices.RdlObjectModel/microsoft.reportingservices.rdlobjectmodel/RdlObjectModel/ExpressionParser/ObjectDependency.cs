using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200023F RID: 575
	internal struct ObjectDependency
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x0002E344 File Offset: 0x0002C544
		public ObjectDependency(ReportElementType inType, ReportObject inDependency)
		{
			this._type = inType;
			this._dependency = inDependency;
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0002E354 File Offset: 0x0002C554
		public ReportElementType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0002E35C File Offset: 0x0002C55C
		public ReportObject Dependency
		{
			get
			{
				return this._dependency;
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0002E364 File Offset: 0x0002C564
		public override bool Equals(object obj)
		{
			if (obj is ObjectDependency)
			{
				ObjectDependency objectDependency = (ObjectDependency)obj;
				if (this.Type == objectDependency.Type && this._dependency == objectDependency.Dependency)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0002E3A1 File Offset: 0x0002C5A1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0002E3B3 File Offset: 0x0002C5B3
		public static bool operator ==(ObjectDependency leftOp, ObjectDependency rightOp)
		{
			return leftOp.Equals(rightOp);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		public static bool operator !=(ObjectDependency leftOp, ObjectDependency rightOp)
		{
			return !leftOp.Equals(rightOp);
		}

		// Token: 0x0400065B RID: 1627
		private readonly ReportElementType _type;

		// Token: 0x0400065C RID: 1628
		private readonly ReportObject _dependency;
	}
}

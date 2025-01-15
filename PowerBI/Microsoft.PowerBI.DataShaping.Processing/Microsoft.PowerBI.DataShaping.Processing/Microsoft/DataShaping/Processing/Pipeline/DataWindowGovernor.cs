using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000099 RID: 153
	internal sealed class DataWindowGovernor
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0000CD65 File Offset: 0x0000AF65
		public DataWindowGovernor()
		{
			this._windows = new List<DataPipelineWindow>();
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000CD78 File Offset: 0x0000AF78
		internal void AddWindow(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow window, int count, int? isExceededDbCount)
		{
			ExceededDetectionKind exceededDetection = window.ExceededDetection;
			if (exceededDetection == ExceededDetectionKind.InstancesVsCount)
			{
				this._windows.Add(new DataPipelineCapacityWindow(window.Id, count, window.TargetScopes));
				return;
			}
			if (exceededDetection != ExceededDetectionKind.DbCountVsCount)
			{
				Contract.RetailFail("Unknown DataWindow.Kind '{0}'", window.ExceededDetection);
				return;
			}
			this._windows.Add(new DataPipelineDbCountVsCountWindow(window.Id, count, isExceededDbCount.Value, window.TargetScopes));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
		internal void ExitInstance(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember)
		{
			if (!this.ActiveMemberHasApplicableWindows)
			{
				return;
			}
			foreach (int num in dataMember.ApplicableWindows)
			{
				this._windows[num].ExitInstance(dataMember);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000CE54 File Offset: 0x0000B054
		internal bool HasCapacity
		{
			get
			{
				if (!this.ActiveMemberHasApplicableWindows)
				{
					return true;
				}
				foreach (int num in this._activeMember.ApplicableWindows)
				{
					if (!this._windows[num].HasCapacity)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		internal bool HasCapacityForMember(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember)
		{
			if (dataMember.ApplicableWindows.IsNullOrEmpty<int>())
			{
				return true;
			}
			foreach (int num in dataMember.ApplicableWindows)
			{
				if (!this._windows[num].HasCapacity)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000CF34 File Offset: 0x0000B134
		internal bool SatisfiesWindowConstraints()
		{
			if (!this.ActiveMemberHasApplicableWindows)
			{
				return true;
			}
			foreach (int num in this._activeMember.ApplicableWindows)
			{
				if (!this._windows[num].SatisfiesWindowConstraints())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		internal bool HasExplicitlyExceededCapacity()
		{
			if (!this.ActiveMemberHasApplicableWindows)
			{
				return false;
			}
			foreach (int num in this._activeMember.ApplicableWindows)
			{
				if (this._windows[num].HasExplicitlyExceededCapacity)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000D014 File Offset: 0x0000B214
		internal void SetHasExceededCapacity()
		{
			if (!this.ActiveMemberHasApplicableWindows)
			{
				return;
			}
			foreach (int num in this._activeMember.ApplicableWindows)
			{
				if (!this._windows[num].HasCapacity)
				{
					this._windows[num].SetHasExceededCapacity();
				}
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D08C File Offset: 0x0000B28C
		private WindowConstraintMode? SetConstraintMode(WindowConstraintMode mode)
		{
			if (!this.ActiveMemberHasApplicableWindows)
			{
				return null;
			}
			using (IEnumerator<int> enumerator = this.ActiveMember.ApplicableWindows.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					return new WindowConstraintMode?(this._windows[num].SetConstraintMode(mode));
				}
			}
			return null;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000D110 File Offset: 0x0000B310
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000D118 File Offset: 0x0000B318
		internal Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember ActiveMember
		{
			get
			{
				return this._activeMember;
			}
			private set
			{
				this._activeMember = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000D124 File Offset: 0x0000B324
		internal WindowConstraintMode? ConstraintMode
		{
			get
			{
				if (!this.ActiveMemberHasApplicableWindows)
				{
					return null;
				}
				using (IEnumerator<int> enumerator = this.ActiveMember.ApplicableWindows.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						return new WindowConstraintMode?(this._windows[num].ConstraintMode);
					}
				}
				return null;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		private bool ActiveMemberHasApplicableWindows
		{
			get
			{
				return this._activeMember != null && this._activeMember.ApplicableWindows != null;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000D1C2 File Offset: 0x0000B3C2
		internal DataPipelineWindow GetWindow(int index)
		{
			return this._windows[index];
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		internal DataWindowToken Update(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember activeMember, WindowConstraintMode? constraintMode)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember activeMember2 = this.ActiveMember;
			this.ActiveMember = activeMember;
			WindowConstraintMode? windowConstraintMode = null;
			if (constraintMode != null)
			{
				windowConstraintMode = this.SetConstraintMode(constraintMode.Value);
			}
			return new DataWindowToken(windowConstraintMode, activeMember2);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D214 File Offset: 0x0000B414
		internal void Restore(in DataWindowToken token)
		{
			this.ActiveMember = token.ActiveMember;
			if (token.ConstraintMode != null)
			{
				this.SetConstraintMode(token.ConstraintMode.Value);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000D252 File Offset: 0x0000B452
		internal List<DataPipelineWindow> Windows
		{
			get
			{
				return this._windows;
			}
		}

		// Token: 0x04000221 RID: 545
		private readonly List<DataPipelineWindow> _windows;

		// Token: 0x04000222 RID: 546
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember _activeMember;
	}
}

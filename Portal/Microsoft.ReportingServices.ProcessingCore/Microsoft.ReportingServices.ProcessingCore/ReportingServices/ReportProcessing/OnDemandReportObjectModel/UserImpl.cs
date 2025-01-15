using System;
using System.Threading;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B5 RID: 1973
	internal sealed class UserImpl : User
	{
		// Token: 0x06007010 RID: 28688 RVA: 0x001D2F6C File Offset: 0x001D116C
		internal UserImpl(string userID, string language, UserProfileState allowUserProfileState, OnDemandProcessingContext odpContext)
		{
			this.m_userID = userID;
			this.m_language = language;
			this.m_allowUserProfileState = allowUserProfileState;
			this.m_odpContext = odpContext;
		}

		// Token: 0x06007011 RID: 28689 RVA: 0x001D2FA4 File Offset: 0x001D11A4
		internal UserImpl(UserImpl copy, OnDemandProcessingContext odpContext)
		{
			this.m_userID = copy.m_userID;
			this.m_language = copy.m_language;
			this.m_allowUserProfileState = copy.m_allowUserProfileState;
			this.m_odpContext = odpContext;
		}

		// Token: 0x17002632 RID: 9778
		public override object this[string key]
		{
			get
			{
				this.UpdateUserProfileState();
				if (key == "UserID")
				{
					return this.m_userID;
				}
				if (!(key == "Language"))
				{
					throw new ReportProcessingException_NonExistingUserReference(key);
				}
				return this.m_language;
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x06007013 RID: 28691 RVA: 0x001D302C File Offset: 0x001D122C
		public override string UserID
		{
			get
			{
				this.UpdateUserProfileState();
				return this.m_userID;
			}
		}

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x06007014 RID: 28692 RVA: 0x001D303A File Offset: 0x001D123A
		public override string Language
		{
			get
			{
				this.UpdateUserProfileState();
				return this.m_language;
			}
		}

		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x06007015 RID: 28693 RVA: 0x001D3048 File Offset: 0x001D1248
		internal UserProfileState UserProfileLocation
		{
			get
			{
				return this.m_location;
			}
		}

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x06007016 RID: 28694 RVA: 0x001D3050 File Offset: 0x001D1250
		internal UserProfileState HasUserProfileState
		{
			get
			{
				return this.m_hasUserProfileState;
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x06007017 RID: 28695 RVA: 0x001D3058 File Offset: 0x001D1258
		// (set) Token: 0x06007018 RID: 28696 RVA: 0x001D3060 File Offset: 0x001D1260
		internal bool IndirectQueryReference
		{
			get
			{
				return this.m_indirectQueryReference;
			}
			set
			{
				this.m_indirectQueryReference = value;
			}
		}

		// Token: 0x06007019 RID: 28697 RVA: 0x001D3069 File Offset: 0x001D1269
		internal IDisposable UpdateUserProfileLocation(UserProfileState newLocation)
		{
			Monitor.Enter(this.m_locationUpdateLock);
			UserImpl.UserProfileTrackingContext userProfileTrackingContext = new UserImpl.UserProfileTrackingContext(this, this.m_location);
			this.m_location = newLocation;
			return userProfileTrackingContext;
		}

		// Token: 0x0600701A RID: 28698 RVA: 0x001D308E File Offset: 0x001D128E
		internal UserProfileState UpdateUserProfileLocationWithoutLocking(UserProfileState newLocation)
		{
			UserProfileState location = this.m_location;
			this.m_location = newLocation;
			return location;
		}

		// Token: 0x0600701B RID: 28699 RVA: 0x001D30A0 File Offset: 0x001D12A0
		private void UpdateUserProfileState()
		{
			Exception ex = null;
			UserProfileState userProfileState = this.m_hasUserProfileState | this.m_location;
			if (this.m_indirectQueryReference)
			{
				userProfileState |= UserProfileState.InQuery;
				if ((this.m_allowUserProfileState & UserProfileState.InQuery) == UserProfileState.None)
				{
					ex = new ReportProcessingException_UserProfilesDependencies();
				}
			}
			if (this.m_location != UserProfileState.OnDemandExpressions && (this.m_allowUserProfileState & this.m_location) == UserProfileState.None)
			{
				ex = new ReportProcessingException_UserProfilesDependencies();
			}
			this.UpdateOverallUserProfileState(ex, userProfileState);
		}

		// Token: 0x0600701C RID: 28700 RVA: 0x001D30FF File Offset: 0x001D12FF
		private void UpdateOverallUserProfileState(Exception exceptionToThrow, UserProfileState newState)
		{
			if (newState != this.m_hasUserProfileState)
			{
				this.m_hasUserProfileState = newState;
				if (exceptionToThrow == null || !this.m_odpContext.InSubreport)
				{
					this.m_odpContext.MergeHasUserProfileState(newState);
				}
			}
			if (exceptionToThrow != null)
			{
				throw exceptionToThrow;
			}
		}

		// Token: 0x0600701D RID: 28701 RVA: 0x001D3134 File Offset: 0x001D1334
		internal void SetConnectionStringUserProfileDependencyOrThrow()
		{
			Exception ex = null;
			using (this.UpdateUserProfileLocation(UserProfileState.InQuery))
			{
				UserProfileState userProfileState = this.m_hasUserProfileState | this.m_location;
				if ((this.m_allowUserProfileState & UserProfileState.InQuery) == UserProfileState.None)
				{
					ex = new ReportProcessingException_UserProfilesDependencies();
				}
				this.UpdateOverallUserProfileState(ex, userProfileState);
			}
		}

		// Token: 0x040039E5 RID: 14821
		private string m_userID;

		// Token: 0x040039E6 RID: 14822
		private string m_language;

		// Token: 0x040039E7 RID: 14823
		private UserProfileState m_allowUserProfileState;

		// Token: 0x040039E8 RID: 14824
		private UserProfileState m_hasUserProfileState;

		// Token: 0x040039E9 RID: 14825
		private UserProfileState m_location = UserProfileState.InReport;

		// Token: 0x040039EA RID: 14826
		private bool m_indirectQueryReference;

		// Token: 0x040039EB RID: 14827
		private object m_locationUpdateLock = new object();

		// Token: 0x040039EC RID: 14828
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x02000CF2 RID: 3314
		private struct UserProfileTrackingContext : IDisposable
		{
			// Token: 0x06008E0F RID: 36367 RVA: 0x002440A9 File Offset: 0x002422A9
			internal UserProfileTrackingContext(UserImpl userImpl, UserProfileState oldLocation)
			{
				this.m_userImpl = userImpl;
				this.m_oldLocation = oldLocation;
			}

			// Token: 0x06008E10 RID: 36368 RVA: 0x002440B9 File Offset: 0x002422B9
			public void Dispose()
			{
				this.m_userImpl.m_location = this.m_oldLocation;
				Monitor.Exit(this.m_userImpl.m_locationUpdateLock);
			}

			// Token: 0x04004FBB RID: 20411
			private UserImpl m_userImpl;

			// Token: 0x04004FBC RID: 20412
			private UserProfileState m_oldLocation;
		}
	}
}

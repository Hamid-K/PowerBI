using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000793 RID: 1939
	internal sealed class UserImpl : User
	{
		// Token: 0x06006C32 RID: 27698 RVA: 0x001B6F48 File Offset: 0x001B5148
		internal UserImpl(string userID, string language, UserProfileState allowUserProfileState)
		{
			this.m_userID = userID;
			this.m_language = language;
			this.m_allowUserProfileState = allowUserProfileState;
		}

		// Token: 0x170025A8 RID: 9640
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
					throw new ArgumentOutOfRangeException("key");
				}
				return this.m_language;
			}
		}

		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x06006C34 RID: 27700 RVA: 0x001B6FA8 File Offset: 0x001B51A8
		public override string UserID
		{
			get
			{
				this.UpdateUserProfileState();
				return this.m_userID;
			}
		}

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x06006C35 RID: 27701 RVA: 0x001B6FB6 File Offset: 0x001B51B6
		public override string Language
		{
			get
			{
				this.UpdateUserProfileState();
				return this.m_language;
			}
		}

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x06006C36 RID: 27702 RVA: 0x001B6FC4 File Offset: 0x001B51C4
		// (set) Token: 0x06006C37 RID: 27703 RVA: 0x001B6FCC File Offset: 0x001B51CC
		internal UserProfileState UserProfileLocation
		{
			get
			{
				return this.m_location;
			}
			set
			{
				this.m_location = value;
			}
		}

		// Token: 0x170025AC RID: 9644
		// (get) Token: 0x06006C38 RID: 27704 RVA: 0x001B6FD5 File Offset: 0x001B51D5
		// (set) Token: 0x06006C39 RID: 27705 RVA: 0x001B6FDD File Offset: 0x001B51DD
		internal UserProfileState HasUserProfileState
		{
			get
			{
				return this.m_hasUserProfileState;
			}
			set
			{
				this.m_hasUserProfileState = value;
			}
		}

		// Token: 0x170025AD RID: 9645
		// (get) Token: 0x06006C3A RID: 27706 RVA: 0x001B6FE6 File Offset: 0x001B51E6
		// (set) Token: 0x06006C3B RID: 27707 RVA: 0x001B6FEE File Offset: 0x001B51EE
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

		// Token: 0x06006C3C RID: 27708 RVA: 0x001B6FF8 File Offset: 0x001B51F8
		internal void UpdateUserProfileState()
		{
			this.m_hasUserProfileState |= this.m_location;
			if (this.m_indirectQueryReference)
			{
				this.m_hasUserProfileState |= UserProfileState.InQuery;
				if ((this.m_allowUserProfileState & UserProfileState.InQuery) == UserProfileState.None)
				{
					throw new ReportProcessingException_UserProfilesDependencies();
				}
			}
			if ((this.m_allowUserProfileState & this.m_location) == UserProfileState.None)
			{
				throw new ReportProcessingException_UserProfilesDependencies();
			}
		}

		// Token: 0x04003656 RID: 13910
		private string m_userID;

		// Token: 0x04003657 RID: 13911
		private string m_language;

		// Token: 0x04003658 RID: 13912
		private UserProfileState m_allowUserProfileState;

		// Token: 0x04003659 RID: 13913
		private UserProfileState m_hasUserProfileState;

		// Token: 0x0400365A RID: 13914
		private UserProfileState m_location = UserProfileState.InReport;

		// Token: 0x0400365B RID: 13915
		private bool m_indirectQueryReference;

		// Token: 0x0400365C RID: 13916
		internal const string Name = "User";

		// Token: 0x0400365D RID: 13917
		internal const string FullName = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.User";
	}
}

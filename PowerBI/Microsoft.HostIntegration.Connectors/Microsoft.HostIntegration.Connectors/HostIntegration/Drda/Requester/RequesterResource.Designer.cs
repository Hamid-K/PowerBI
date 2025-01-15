using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200099B RID: 2459
	internal class RequesterResource
	{
		// Token: 0x06004C16 RID: 19478 RVA: 0x00002061 File Offset: 0x00000261
		private RequesterResource()
		{
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06004C17 RID: 19479 RVA: 0x00130969 File Offset: 0x0012EB69
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (RequesterResource.resourceManager == null)
				{
					RequesterResource.resourceManager = new ResourceManager("Microsoft.HostIntegration.Drda.Requester.RequesterResource", typeof(RequesterResource).Assembly);
				}
				return RequesterResource.resourceManager;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x00130995 File Offset: 0x0012EB95
		// (set) Token: 0x06004C19 RID: 19481 RVA: 0x0013099C File Offset: 0x0012EB9C
		internal static CultureInfo Culture
		{
			get
			{
				return RequesterResource.resourceCulture;
			}
			set
			{
				RequesterResource.resourceCulture = value;
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x001309A4 File Offset: 0x0012EBA4
		internal static string AuthenticationNotSupported
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("AuthenticationNotSupported", RequesterResource.Culture);
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x001309BA File Offset: 0x0012EBBA
		internal static string DDM_AGNPRMRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_AGNPRMRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x001309D0 File Offset: 0x0012EBD0
		internal static string DDM_CMDATHRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_CMDATHRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x001309E6 File Offset: 0x0012EBE6
		internal static string DDM_CMDCHKRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_CMDCHKRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06004C1E RID: 19486 RVA: 0x001309FC File Offset: 0x0012EBFC
		internal static string DDM_CMDNSPRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_CMDNSPRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x00130A12 File Offset: 0x0012EC12
		internal static string DDM_DSCINVRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_DSCINVRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06004C20 RID: 19488 RVA: 0x00130A28 File Offset: 0x0012EC28
		internal static string DDM_DTAMCHRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_DTAMCHRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x00130A3E File Offset: 0x0012EC3E
		internal static string DDM_MGRDEPRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_MGRDEPRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06004C22 RID: 19490 RVA: 0x00130A54 File Offset: 0x0012EC54
		internal static string DDM_OBJNSPRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_OBJNSPRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x00130A6A File Offset: 0x0012EC6A
		internal static string DDM_OPNQFLRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_OPNQFLRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06004C24 RID: 19492 RVA: 0x00130A80 File Offset: 0x0012EC80
		internal static string DDM_PRCCNVRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_PRCCNVRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x00130A96 File Offset: 0x0012EC96
		internal static string DDM_PKGBPARM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_PKGBPARM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06004C26 RID: 19494 RVA: 0x00130AAC File Offset: 0x0012ECAC
		internal static string DDM_PRMNSPRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_PRMNSPRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x00130AC2 File Offset: 0x0012ECC2
		internal static string DDM_QRYNOPRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_QRYNOPRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06004C28 RID: 19496 RVA: 0x00130AD8 File Offset: 0x0012ECD8
		internal static string DDM_RDBACCRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_RDBACCRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06004C29 RID: 19497 RVA: 0x00130AEE File Offset: 0x0012ECEE
		internal static string DDM_RDBAFLRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_RDBAFLRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x00130B04 File Offset: 0x0012ED04
		internal static string DDM_RDBATHRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_RDBATHRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x00130B1A File Offset: 0x0012ED1A
		internal static string DDM_RDBNFNRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_RDBNFNRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06004C2C RID: 19500 RVA: 0x00130B30 File Offset: 0x0012ED30
		internal static string DDM_RSCLMTRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_RSCLMTRM", RequesterResource.Culture);
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x00130B46 File Offset: 0x0012ED46
		internal static string DDM_SQLERRRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_SQLERRRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06004C2E RID: 19502 RVA: 0x00130B5C File Offset: 0x0012ED5C
		internal static string DDM_SYNTAXRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_SYNTAXRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x00130B72 File Offset: 0x0012ED72
		internal static string DDM_TRGNSPRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_TRGNSPRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x00130B88 File Offset: 0x0012ED88
		internal static string DDM_VALNSPRM
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DDM_VALNSPRM", RequesterResource.Culture);
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06004C31 RID: 19505 RVA: 0x00130B9E File Offset: 0x0012ED9E
		internal static string DtcTimeout
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("DtcTimeout", RequesterResource.Culture);
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x00130BB4 File Offset: 0x0012EDB4
		internal static string EmptyRows
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("EmptyRows", RequesterResource.Culture);
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x00130BCA File Offset: 0x0012EDCA
		internal static string MaxSectionsReached
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("MaxSectionsReached", RequesterResource.Culture);
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x00130BE0 File Offset: 0x0012EDE0
		internal static string NETWORK_CONNECT_TIMEOUT
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("NETWORK_CONNECT_TIMEOUT", RequesterResource.Culture);
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x00130BF6 File Offset: 0x0012EDF6
		internal static string NoCurrentResultSet
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("NoCurrentResultSet", RequesterResource.Culture);
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x00130C0C File Offset: 0x0012EE0C
		internal static string NoEnlistForLocalTransaction
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("NoEnlistForLocalTransaction", RequesterResource.Culture);
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x00130C22 File Offset: 0x0012EE22
		internal static string PackageNotPresent
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("PackageNotPresent", RequesterResource.Culture);
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x00130C38 File Offset: 0x0012EE38
		internal static string RequesterNotConnected
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("RequesterNotConnected", RequesterResource.Culture);
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x00130C4E File Offset: 0x0012EE4E
		internal static string SqlStatementInUse
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("SqlStatementInUse", RequesterResource.Culture);
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x00130C64 File Offset: 0x0012EE64
		internal static string SqlStatementStateNoChange
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("SqlStatementStateNoChange", RequesterResource.Culture);
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x00130C7A File Offset: 0x0012EE7A
		internal static string UnauthorizedAccess
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("UnauthorizedAccess", RequesterResource.Culture);
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x00130C90 File Offset: 0x0012EE90
		internal static string MultipleStatementsNotSupported
		{
			get
			{
				return RequesterResource.ResourceManager.GetString("MultipleStatementsNotSupported", RequesterResource.Culture);
			}
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x00130CA6 File Offset: 0x0012EEA6
		internal static string ColumnInfoNoSet(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("ColumnInfoNoSet", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x00130CC7 File Offset: 0x0012EEC7
		internal static string CopiedPackage(object param0, object param1, object param2, object param3, object param4)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CopiedPackage", RequesterResource.Culture), new object[] { param0, param1, param2, param3, param4 });
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x00130D02 File Offset: 0x0012EF02
		internal static string CopyingPackage(object param0, object param1, object param2, object param3, object param4)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CopyingPackage", RequesterResource.Culture), new object[] { param0, param1, param2, param3, param4 });
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x00130D3D File Offset: 0x0012EF3D
		internal static string CopyingPackageFailed(object param0, object param1, object param2, object param3, object param4)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CopyingPackageFailed", RequesterResource.Culture), new object[] { param0, param1, param2, param3, param4 });
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x00130D78 File Offset: 0x0012EF78
		internal static string CreatedPackage(object param0, object param1, object param2)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CreatedPackage", RequesterResource.Culture), param0, param1, param2);
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x00130D9B File Offset: 0x0012EF9B
		internal static string CreatedSection(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CreatedSection", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x00130DBC File Offset: 0x0012EFBC
		internal static string CreatingPackage(object param0, object param1, object param2)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CreatingPackage", RequesterResource.Culture), param0, param1, param2);
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x00130DDF File Offset: 0x0012EFDF
		internal static string CreatingPackageFailed(object param0, object param1, object param2)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CreatingPackageFailed", RequesterResource.Culture), param0, param1, param2);
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x00130E02 File Offset: 0x0012F002
		internal static string CreatingSection(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("CreatingSection", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x00130E23 File Offset: 0x0012F023
		internal static string DeployedPackage(object param0, object param1, object param2, object param3, object param4)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("DeployedPackage", RequesterResource.Culture), new object[] { param0, param1, param2, param3, param4 });
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x00130E5E File Offset: 0x0012F05E
		internal static string DeployingPackage(object param0, object param1, object param2, object param3, object param4)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("DeployingPackage", RequesterResource.Culture), new object[] { param0, param1, param2, param3, param4 });
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x00130E99 File Offset: 0x0012F099
		internal static string DeployingPackageFailed(object param0, object param1, object param2, object param3, object param4)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("DeployingPackageFailed", RequesterResource.Culture), new object[] { param0, param1, param2, param3, param4 });
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x00130ED4 File Offset: 0x0012F0D4
		internal static string DroppedPackage(object param0, object param1, object param2)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("DroppedPackage", RequesterResource.Culture), param0, param1, param2);
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x00130EF7 File Offset: 0x0012F0F7
		internal static string DroppingPackage(object param0, object param1, object param2)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("DroppingPackage", RequesterResource.Culture), param0, param1, param2);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x00130F1A File Offset: 0x0012F11A
		internal static string DroppingPackageFailed(object param0, object param1, object param2)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("DroppingPackageFailed", RequesterResource.Culture), param0, param1, param2);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x00130F3D File Offset: 0x0012F13D
		internal static string InvalidStaticPackageName(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("InvalidStaticPackageName", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x00130F5E File Offset: 0x0012F15E
		internal static string KerberosInitializeError(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("KerberosInitializeError", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x00130F7F File Offset: 0x0012F17F
		internal static string KerberosValidateError(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("KerberosValidateError", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x00130FA0 File Offset: 0x0012F1A0
		internal static string LobNotSupported(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("LobNotSupported", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x00130FC1 File Offset: 0x0012F1C1
		internal static string NoExpectedCodepoint(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("NoExpectedCodepoint", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x00130FE2 File Offset: 0x0012F1E2
		internal static string RebindedPackage(object param0, object param1, object param2, object param3)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("RebindedPackage", RequesterResource.Culture), new object[] { param0, param1, param2, param3 });
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x00131018 File Offset: 0x0012F218
		internal static string RebindingPackage(object param0, object param1, object param2, object param3)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("RebindingPackage", RequesterResource.Culture), new object[] { param0, param1, param2, param3 });
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0013104E File Offset: 0x0012F24E
		internal static string RebindingPackageFailed(object param0, object param1, object param2, object param3)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("RebindingPackageFailed", RequesterResource.Culture), new object[] { param0, param1, param2, param3 });
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x00131084 File Offset: 0x0012F284
		internal static string TypeNotSupported(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("TypeNotSupported", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x001310A5 File Offset: 0x0012F2A5
		internal static string UnexpectedXaResult(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("UnexpectedXaResult", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x001310C6 File Offset: 0x0012F2C6
		internal static string UnsupportedManagerLevel(object param0, object param1)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("UnsupportedManagerLevel", RequesterResource.Culture), param0, param1);
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x001310E8 File Offset: 0x0012F2E8
		internal static string WrongDateTimeValue(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("WrongDateTimeValue", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x00131109 File Offset: 0x0012F309
		internal static string WrongDateValue(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("WrongDateValue", RequesterResource.Culture), param0);
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0013112A File Offset: 0x0012F32A
		internal static string WrongTimeValue(object param0)
		{
			return string.Format(RequesterResource.Culture, RequesterResource.ResourceManager.GetString("WrongTimeValue", RequesterResource.Culture), param0);
		}

		// Token: 0x04003C1D RID: 15389
		private static ResourceManager resourceManager;

		// Token: 0x04003C1E RID: 15390
		private static CultureInfo resourceCulture;
	}
}

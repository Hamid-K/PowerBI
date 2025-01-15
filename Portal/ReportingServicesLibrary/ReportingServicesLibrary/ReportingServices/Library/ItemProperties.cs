using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200007A RID: 122
	internal sealed class ItemProperties : PropertyCollection
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00014A8E File Offset: 0x00012C8E
		internal ItemProperties()
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00014A98 File Offset: 0x00012C98
		internal ItemProperties(Property[] properties, ItemType itemType)
			: base(properties)
		{
			string description = this.Description;
			if (description != null && description.Length > 512)
			{
				throw new InvalidElementException("Description");
			}
			base.ValidateReportTimeoutIfPresent("ReportTimeout", itemType);
			base.ValidateIntegerProperty("QueryExecutionTimeOut", this.QueryExecutionTimeOut, false);
			base.ValidateDoubleProperty("PageWidth", this.PageWidth);
			base.ValidateDoubleProperty("PageHeight", this.PageHeight);
			base.ValidateDoubleProperty("LeftMargin", this.LeftMargin);
			base.ValidateDoubleProperty("RightMargin", this.RightMargin);
			base.ValidateDoubleProperty("TopMargin", this.TopMargin);
			base.ValidateDoubleProperty("BottomMargin", this.BottomMargin);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00014B52 File Offset: 0x00012D52
		internal ItemProperties(string propertiesXml)
			: base(propertiesXml)
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00014B5B File Offset: 0x00012D5B
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00014B68 File Offset: 0x00012D68
		internal string Name
		{
			get
			{
				return base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00014B76 File Offset: 0x00012D76
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00014B83 File Offset: 0x00012D83
		internal string RightMargin
		{
			get
			{
				return base["RightMargin"];
			}
			set
			{
				base["RightMargin"] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00014B91 File Offset: 0x00012D91
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00014B9E File Offset: 0x00012D9E
		internal string LeftMargin
		{
			get
			{
				return base["LeftMargin"];
			}
			set
			{
				base["LeftMargin"] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00014BAC File Offset: 0x00012DAC
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00014BB9 File Offset: 0x00012DB9
		internal string TopMargin
		{
			get
			{
				return base["TopMargin"];
			}
			set
			{
				base["TopMargin"] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00014BC7 File Offset: 0x00012DC7
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00014BD4 File Offset: 0x00012DD4
		internal string BottomMargin
		{
			get
			{
				return base["BottomMargin"];
			}
			set
			{
				base["BottomMargin"] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00014BE2 File Offset: 0x00012DE2
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00014BEF File Offset: 0x00012DEF
		internal string PageHeight
		{
			get
			{
				return base["PageHeight"];
			}
			set
			{
				base["PageHeight"] = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00014BFD File Offset: 0x00012DFD
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00014C0A File Offset: 0x00012E0A
		internal string PageWidth
		{
			get
			{
				return base["PageWidth"];
			}
			set
			{
				base["PageWidth"] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00014C18 File Offset: 0x00012E18
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00014C25 File Offset: 0x00012E25
		internal string Path
		{
			get
			{
				return base["Path"];
			}
			set
			{
				base["Path"] = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00014C33 File Offset: 0x00012E33
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00014C40 File Offset: 0x00012E40
		internal string ParentID
		{
			get
			{
				return base["ParentID"];
			}
			set
			{
				base["ParentID"] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00014C4E File Offset: 0x00012E4E
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00014C5B File Offset: 0x00012E5B
		internal string Type
		{
			get
			{
				return base["Type"];
			}
			set
			{
				base["Type"] = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00014C69 File Offset: 0x00012E69
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00014C76 File Offset: 0x00012E76
		internal string Size
		{
			get
			{
				return base["Size"];
			}
			set
			{
				base["Size"] = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00014C84 File Offset: 0x00012E84
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00014C91 File Offset: 0x00012E91
		internal string ID
		{
			get
			{
				return base["ID"];
			}
			set
			{
				base["ID"] = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00014C9F File Offset: 0x00012E9F
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00014CAC File Offset: 0x00012EAC
		internal string Description
		{
			get
			{
				return base["Description"];
			}
			set
			{
				base["Description"] = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00014CBA File Offset: 0x00012EBA
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00014CC7 File Offset: 0x00012EC7
		internal string CreatedBy
		{
			get
			{
				return base["CreatedBy"];
			}
			set
			{
				base["CreatedBy"] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00014CD5 File Offset: 0x00012ED5
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00014CE2 File Offset: 0x00012EE2
		internal string CreationDate
		{
			get
			{
				return base["CreationDate"];
			}
			set
			{
				base["CreationDate"] = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00014CF0 File Offset: 0x00012EF0
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00014CFD File Offset: 0x00012EFD
		internal string ModifiedBy
		{
			get
			{
				return base["ModifiedBy"];
			}
			set
			{
				base["ModifiedBy"] = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00014D0B File Offset: 0x00012F0B
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00014D18 File Offset: 0x00012F18
		internal string ModifiedDate
		{
			get
			{
				return base["ModifiedDate"];
			}
			set
			{
				base["ModifiedDate"] = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00014D26 File Offset: 0x00012F26
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00014D33 File Offset: 0x00012F33
		internal string MimeType
		{
			get
			{
				return base["MIMEType"];
			}
			set
			{
				base["MIMEType"] = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00014D41 File Offset: 0x00012F41
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00014D4E File Offset: 0x00012F4E
		internal string CanRunUnattended
		{
			get
			{
				return base["CanRunUnattended"];
			}
			set
			{
				base["CanRunUnattended"] = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00014D5C File Offset: 0x00012F5C
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00014D69 File Offset: 0x00012F69
		internal string HasParameterDefaultValues
		{
			get
			{
				return base["HasParameterDefaultValues"];
			}
			set
			{
				base["HasParameterDefaultValues"] = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00014D77 File Offset: 0x00012F77
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00014D84 File Offset: 0x00012F84
		internal string HasScheduleReadyDataSources
		{
			get
			{
				return base["HasScheduleReadyDataSources"];
			}
			set
			{
				base["HasScheduleReadyDataSources"] = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00014D92 File Offset: 0x00012F92
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00014D9F File Offset: 0x00012F9F
		internal string HasUserProfileQueryDependencies
		{
			private get
			{
				return base["HasUserProfileQueryDependencies"];
			}
			set
			{
				base["HasUserProfileQueryDependencies"] = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00014DAD File Offset: 0x00012FAD
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00014DBA File Offset: 0x00012FBA
		internal string HasUserProfileReportDependencies
		{
			private get
			{
				return base["HasUserProfileReportDependencies"];
			}
			set
			{
				base["HasUserProfileReportDependencies"] = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00014DC8 File Offset: 0x00012FC8
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00014DD5 File Offset: 0x00012FD5
		internal string VirtualPath
		{
			get
			{
				return base["VirtualPath"];
			}
			set
			{
				base["VirtualPath"] = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00014DE3 File Offset: 0x00012FE3
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00014DF0 File Offset: 0x00012FF0
		internal string ExecutionDate
		{
			get
			{
				return base["ExecutionDateTime"];
			}
			set
			{
				base["ExecutionDateTime"] = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00014DFE File Offset: 0x00012FFE
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00014E0B File Offset: 0x0001300B
		internal string Reserved
		{
			get
			{
				return base["Reserved"];
			}
			set
			{
				base["Reserved"] = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00014E19 File Offset: 0x00013019
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00014E26 File Offset: 0x00013026
		internal string Language
		{
			get
			{
				return base["Language"];
			}
			set
			{
				base["Language"] = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00014E34 File Offset: 0x00013034
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00014E41 File Offset: 0x00013041
		internal string Hidden
		{
			get
			{
				return base["Hidden"];
			}
			set
			{
				base["Hidden"] = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00014E4F File Offset: 0x0001304F
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00014E5C File Offset: 0x0001305C
		internal string ReportTimeout
		{
			get
			{
				return base["ReportTimeout"];
			}
			set
			{
				base["ReportTimeout"] = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00014E6A File Offset: 0x0001306A
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00014E77 File Offset: 0x00013077
		internal string IsSnapshotExecution
		{
			get
			{
				return base["IsSnapshotExecution"];
			}
			set
			{
				base["IsSnapshotExecution"] = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00014E85 File Offset: 0x00013085
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00014E92 File Offset: 0x00013092
		internal string HasValidReportLink
		{
			get
			{
				return base["HasValidReportLink"];
			}
			set
			{
				base["HasValidReportLink"] = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00014EA0 File Offset: 0x000130A0
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00014EAD File Offset: 0x000130AD
		internal string IsAutoGenerated
		{
			get
			{
				return base["IsGenerated"];
			}
			set
			{
				base["IsGenerated"] = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00014EBB File Offset: 0x000130BB
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00014EC8 File Offset: 0x000130C8
		internal string CanGenerateModel
		{
			get
			{
				return base["CanGenerateModel"];
			}
			set
			{
				base["CanGenerateModel"] = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00014ED6 File Offset: 0x000130D6
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00014EE3 File Offset: 0x000130E3
		internal string Rdce
		{
			get
			{
				return base["RDCE"];
			}
			set
			{
				base["RDCE"] = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00014EF1 File Offset: 0x000130F1
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00014EFE File Offset: 0x000130FE
		internal string ComponentID
		{
			get
			{
				return base["ComponentID"];
			}
			set
			{
				base["ComponentID"] = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00014F0C File Offset: 0x0001310C
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00014F19 File Offset: 0x00013119
		internal string SubType
		{
			get
			{
				return base["Subtype"];
			}
			set
			{
				base["Subtype"] = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00014F27 File Offset: 0x00013127
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00014F34 File Offset: 0x00013134
		internal string QueryExecutionTimeOut
		{
			get
			{
				return base["QueryExecutionTimeOut"];
			}
			set
			{
				base["QueryExecutionTimeOut"] = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00014F42 File Offset: 0x00013142
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00014F4F File Offset: 0x0001314F
		internal string HasDataSourceCredentials
		{
			get
			{
				return base["HasDataSourceCredentials"];
			}
			set
			{
				base["HasDataSourceCredentials"] = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00014F5D File Offset: 0x0001315D
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00014F6A File Offset: 0x0001316A
		internal string HasUserProfileDependencies
		{
			get
			{
				return base["HasUserProfileDependencies"];
			}
			set
			{
				base["HasUserProfileDependencies"] = value;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00014F78 File Offset: 0x00013178
		internal bool FixupIfConnectionStringUserDependency(DataSourceInfoCollection dataSources)
		{
			if (dataSources != null && dataSources.HasConnectionStringUseridReference())
			{
				this.HasUserProfileQueryDependencies = "true";
				return true;
			}
			return false;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00014F93 File Offset: 0x00013193
		internal bool FixupIfConnectionStringUserDependency(RuntimeDataSourceInfoCollection runtimeDataSources)
		{
			if (runtimeDataSources != null && runtimeDataSources.HasConnectionStringUseridReference())
			{
				this.HasUserProfileQueryDependencies = "true";
				return true;
			}
			return false;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00014FB0 File Offset: 0x000131B0
		internal bool QueryDependsOnUser
		{
			get
			{
				bool flag = Localization.CatalogCultureCompare(this.HasUserProfileQueryDependencies, bool.TrueString) == 0;
				if (!flag && Globals.Configuration.IsRdceEnabled)
				{
					flag = !string.IsNullOrEmpty(this.Rdce);
				}
				return flag;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00014FF0 File Offset: 0x000131F0
		internal bool LayoutDependsOnUser
		{
			get
			{
				bool flag = Localization.CatalogCultureCompare(this.HasUserProfileReportDependencies, bool.TrueString) == 0;
				if (!flag && Globals.Configuration.IsRdceEnabled)
				{
					flag = !string.IsNullOrEmpty(this.Rdce);
				}
				return flag;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00015030 File Offset: 0x00013230
		internal UserProfileState DependsOnUser
		{
			get
			{
				UserProfileState userProfileState = UserProfileState.None;
				if (this.QueryDependsOnUser)
				{
					userProfileState |= UserProfileState.InQuery;
				}
				if (this.LayoutDependsOnUser)
				{
					userProfileState |= UserProfileState.InReport;
				}
				return userProfileState;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00015058 File Offset: 0x00013258
		internal void AddStoredProperties(string name, ItemType type, int size, Guid id, string createdBy, DateTime creationDate, string modifiedBy, DateTime modifiedDate, string mimeType, string description, DateTime executionDateTime, bool hidden, Guid linkID, Guid? componentID, string subType, Guid? parentID)
		{
			this.Name = name;
			this.Type = type.ToString();
			this.Size = size.ToString(CultureInfo.InvariantCulture);
			this.ID = id.ToString();
			this.CreatedBy = createdBy;
			this.CreationDate = Globals.ToPublicDateTimeFormat(creationDate);
			this.ModifiedBy = modifiedBy;
			this.ModifiedDate = Globals.ToPublicDateTimeFormat(modifiedDate);
			this.MimeType = mimeType;
			this.Description = description;
			if (executionDateTime != DateTime.MinValue)
			{
				this.ExecutionDate = Globals.ToPublicDateTimeFormat(executionDateTime);
			}
			this.Hidden = hidden.ToString();
			if (type == ItemType.LinkedReport)
			{
				this.HasValidReportLink = (linkID != Guid.Empty).ToString();
			}
			if (componentID != null)
			{
				this.ComponentID = ItemProperties.ComponentIDToString(componentID.Value);
			}
			if (subType != null)
			{
				this.SubType = subType;
			}
			if (parentID != null)
			{
				this.ParentID = ItemProperties.ParentIDToString(new Guid?(parentID.Value));
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001516B File Offset: 0x0001336B
		internal static string ComponentIDToString(Guid componentID)
		{
			return componentID.ToString("D");
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001517C File Offset: 0x0001337C
		internal static string ParentIDToString(Guid? parentID)
		{
			if (parentID != null)
			{
				return parentID.Value.ToString("D");
			}
			return null;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000151A8 File Offset: 0x000133A8
		internal static string PrepareForSaving(ItemProperties properties, out string description, out bool hidden)
		{
			description = null;
			hidden = false;
			if (properties == null)
			{
				return null;
			}
			description = properties.Description;
			string hidden2 = properties.Hidden;
			if (hidden2 != null)
			{
				hidden = bool.Parse(hidden2);
			}
			for (int i = 0; i < ItemPropertyNames.SpecialPropertyNames.Length; i++)
			{
				properties.Remove(ItemPropertyNames.SpecialPropertyNames[i]);
			}
			return properties.ToXml();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000151FF File Offset: 0x000133FF
		protected override bool IsReadOnlyProperty(string propertyName)
		{
			return ItemPropertyNames.ItemReadOnlyProperties.Contains(propertyName);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001520C File Offset: 0x0001340C
		internal void Protect(string propertyName)
		{
			if (!string.IsNullOrEmpty(base[propertyName]))
			{
				base[propertyName] = CatalogEncryption.Instance.EncryptToString(base[propertyName], null);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00015235 File Offset: 0x00013435
		internal void UnProtect(string propertyName)
		{
			if (!string.IsNullOrEmpty(base[propertyName]))
			{
				base[propertyName] = CatalogEncryption.Instance.DecryptToString(base[propertyName], null);
			}
		}
	}
}

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public sealed class SqlClientPermission : DBDataPermission
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x0003325E File Offset: 0x0003145E
		[Obsolete("SqlClientPermission() has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission()
			: this(PermissionState.None)
		{
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00033267 File Offset: 0x00031467
		public SqlClientPermission(PermissionState state)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor(state);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003327B File Offset: 0x0003147B
		[Obsolete("SqlClientPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003328B File Offset: 0x0003148B
		private SqlClientPermission(SqlClientPermission permission)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor(permission);
			this.CopyFrom(permission);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000332A6 File Offset: 0x000314A6
		internal SqlClientPermission(SqlClientPermissionAttribute permissionAttribute)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor(permissionAttribute);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000332BC File Offset: 0x000314BC
		internal SqlClientPermission(SqlConnectionString constr)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor(PermissionState.None);
			if (constr != null)
			{
				base.AllowBlankPassword = constr.HasBlankPassword;
				this.AddPermissionEntry(new DBConnectionString(constr));
			}
			if (constr == null || constr.IsEmpty)
			{
				base.Add("", "", KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00033314 File Offset: 0x00031514
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString dbconnectionString = new DBConnectionString(connectionString, restrictions, behavior, SqlConnectionString.GetParseSynonyms(), false);
			this.AddPermissionEntry(dbconnectionString);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00033337 File Offset: 0x00031537
		public override IPermission Copy()
		{
			return new SqlClientPermission(this);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00033340 File Offset: 0x00031540
		internal void AddPermissionEntry(DBConnectionString entry)
		{
			if (this._keyvaluetree == null)
			{
				this._keyvaluetree = new NameValuePermission();
			}
			if (this._keyvalues == null)
			{
				this._keyvalues = new ArrayList();
			}
			NameValuePermission.AddEntry(this._keyvaluetree, this._keyvalues, entry);
			this._IsUnrestricted = false;
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000333BE File Offset: 0x000315BE
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x0003338C File Offset: 0x0003158C
		private bool _IsUnrestricted
		{
			get
			{
				return base.IsUnrestricted();
			}
			set
			{
				FieldInfo field = base.GetType().BaseType.GetField("_isUnrestricted", BindingFlags.Instance | BindingFlags.NonPublic);
				field.SetValue(this, value);
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000333C8 File Offset: 0x000315C8
		private void CopyFrom(SqlClientPermission permission)
		{
			if (!this._IsUnrestricted && permission._keyvalues != null)
			{
				this._keyvalues = (ArrayList)permission._keyvalues.Clone();
				if (permission._keyvaluetree != null)
				{
					this._keyvaluetree = permission._keyvaluetree.CopyNameValue();
				}
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00033414 File Offset: 0x00031614
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (base.IsUnrestricted())
			{
				return target.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target;
			if (dbdataPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			SqlClientPermission sqlClientPermission = (SqlClientPermission)dbdataPermission.Copy();
			sqlClientPermission.AllowBlankPassword &= base.AllowBlankPassword;
			if (this._keyvalues != null && sqlClientPermission._keyvalues != null)
			{
				sqlClientPermission._keyvalues.Clear();
				sqlClientPermission._keyvaluetree.Intersect(sqlClientPermission._keyvalues, this._keyvaluetree);
			}
			else
			{
				sqlClientPermission._keyvalues = null;
				sqlClientPermission._keyvaluetree = null;
			}
			if (sqlClientPermission.IsEmpty())
			{
				sqlClientPermission = null;
			}
			return sqlClientPermission;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000334D0 File Offset: 0x000316D0
		private bool IsEmpty()
		{
			ArrayList keyvalues = this._keyvalues;
			return !base.IsUnrestricted() && !base.AllowBlankPassword && (keyvalues == null || keyvalues.Count == 0);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00033508 File Offset: 0x00031708
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			SqlClientPermission sqlClientPermission = target as SqlClientPermission;
			bool flag = sqlClientPermission.IsUnrestricted();
			if (!flag && !base.IsUnrestricted() && (!base.AllowBlankPassword || sqlClientPermission.AllowBlankPassword) && (this._keyvalues == null || sqlClientPermission._keyvaluetree != null))
			{
				flag = true;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						if (!sqlClientPermission._keyvaluetree.CheckValueForKeyPermit(dbconnectionString))
						{
							flag = false;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000335D8 File Offset: 0x000317D8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (base.IsUnrestricted())
			{
				return this.Copy();
			}
			SqlClientPermission sqlClientPermission = (SqlClientPermission)target.Copy();
			if (!sqlClientPermission.IsUnrestricted())
			{
				sqlClientPermission.AllowBlankPassword |= base.AllowBlankPassword;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						sqlClientPermission.AddPermissionEntry(dbconnectionString);
					}
				}
			}
			if (!sqlClientPermission.IsEmpty())
			{
				return sqlClientPermission;
			}
			return null;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x000336A0 File Offset: 0x000318A0
		private string DecodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace("&quot;", "\"");
				value = value.Replace("&apos;", "'");
				value = value.Replace("&lt;", "<");
				value = value.Replace("&gt;", ">");
				value = value.Replace("&amp;", "&");
			}
			return value;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00033714 File Offset: 0x00031914
		private string EncodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace('\0', ' ');
				value = value.Trim();
				value = value.Replace("&", "&amp;");
				value = value.Replace(">", "&gt;");
				value = value.Replace("<", "&lt;");
				value = value.Replace("'", "&apos;");
				value = value.Replace("\"", "&quot;");
			}
			return value;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003379C File Offset: 0x0003199C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw ADP.ArgumentNull("securityElement");
			}
			string text = securityElement.Tag;
			if (!text.Equals("Permission") && !text.Equals("IPermission"))
			{
				throw ADP.NotAPermissionElement();
			}
			string text2 = securityElement.Attribute("version");
			if (text2 != null && !text2.Equals("1"))
			{
				throw ADP.InvalidXMLBadVersion();
			}
			string text3 = securityElement.Attribute("Unrestricted");
			this._IsUnrestricted = text3 != null && bool.Parse(text3);
			base.Clear();
			if (!this._IsUnrestricted)
			{
				string text4 = securityElement.Attribute("AllowBlankPassword");
				base.AllowBlankPassword = text4 != null && bool.Parse(text4);
				ArrayList children = securityElement.Children;
				if (children == null)
				{
					return;
				}
				using (IEnumerator enumerator = children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement2 = (SecurityElement)obj;
						text = securityElement2.Tag;
						if ("add" == text || (text != null && "add" == text.ToLower(CultureInfo.InvariantCulture)))
						{
							string text5 = securityElement2.Attribute("ConnectionString");
							string text6 = securityElement2.Attribute("KeyRestrictions");
							string text7 = securityElement2.Attribute("KeyRestrictionBehavior");
							KeyRestrictionBehavior keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
							if (text7 != null)
							{
								keyRestrictionBehavior = (KeyRestrictionBehavior)Enum.Parse(typeof(KeyRestrictionBehavior), text7, true);
							}
							text5 = this.DecodeXmlValue(text5);
							text6 = this.DecodeXmlValue(text6);
							this.Add(text5, text6, keyRestrictionBehavior);
						}
					}
					return;
				}
			}
			base.AllowBlankPassword = false;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00033950 File Offset: 0x00031B50
		public override SecurityElement ToXml()
		{
			Type type = base.GetType();
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.AssemblyQualifiedName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (base.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("AllowBlankPassword", base.AllowBlankPassword.ToString(CultureInfo.InvariantCulture));
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						SecurityElement securityElement2 = new SecurityElement("add");
						string text = dbconnectionString.ConnectionString;
						text = this.EncodeXmlValue(text);
						if (!ADP.IsEmpty(text))
						{
							securityElement2.AddAttribute("ConnectionString", text);
						}
						text = dbconnectionString.Restrictions;
						text = this.EncodeXmlValue(text);
						if (text == null)
						{
							text = "";
						}
						securityElement2.AddAttribute("KeyRestrictions", text);
						text = dbconnectionString.Behavior.ToString();
						securityElement2.AddAttribute("KeyRestrictionBehavior", text);
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x0400069A RID: 1690
		private NameValuePermission _keyvaluetree;

		// Token: 0x0400069B RID: 1691
		private ArrayList _keyvalues;

		// Token: 0x02000214 RID: 532
		private static class XmlStr
		{
			// Token: 0x04001597 RID: 5527
			internal const string _class = "class";

			// Token: 0x04001598 RID: 5528
			internal const string _IPermission = "IPermission";

			// Token: 0x04001599 RID: 5529
			internal const string _Permission = "Permission";

			// Token: 0x0400159A RID: 5530
			internal const string _Unrestricted = "Unrestricted";

			// Token: 0x0400159B RID: 5531
			internal const string _AllowBlankPassword = "AllowBlankPassword";

			// Token: 0x0400159C RID: 5532
			internal const string _true = "true";

			// Token: 0x0400159D RID: 5533
			internal const string _Version = "version";

			// Token: 0x0400159E RID: 5534
			internal const string _VersionNumber = "1";

			// Token: 0x0400159F RID: 5535
			internal const string _add = "add";

			// Token: 0x040015A0 RID: 5536
			internal const string _ConnectionString = "ConnectionString";

			// Token: 0x040015A1 RID: 5537
			internal const string _KeyRestrictions = "KeyRestrictions";

			// Token: 0x040015A2 RID: 5538
			internal const string _KeyRestrictionBehavior = "KeyRestrictionBehavior";
		}
	}
}

using System;
using System.Collections;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000023 RID: 35
	internal sealed class SdAndType
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003CDA File Offset: 0x00001EDA
		internal SdAndType()
		{
			this.m_secDescType = SecDescType.Invalid;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003CED File Offset: 0x00001EED
		internal SdAndType(SecDescType secDescType, byte[] secDesc)
		{
			this.m_secDescType = secDescType;
			this.m_secDesc = secDesc;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003D03 File Offset: 0x00001F03
		internal byte Type2Marker()
		{
			if (this.m_secDescType == SecDescType.Invalid)
			{
				throw new InternalCatalogException("Type2Marker: Invalid security descriptor type.");
			}
			return (byte)this.m_secDescType;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003D24 File Offset: 0x00001F24
		internal void Marker2Type(byte marker)
		{
			if (marker == SdAndType.Catalog)
			{
				this.m_secDescType = SecDescType.Catalog;
				return;
			}
			if (marker == SdAndType.Folder)
			{
				this.m_secDescType = SecDescType.Folder;
				return;
			}
			if (marker == SdAndType.Report)
			{
				this.m_secDescType = SecDescType.ReportPrimary;
				return;
			}
			if (marker == SdAndType.ReportSecondary)
			{
				this.m_secDescType = SecDescType.ReportSecondary;
				return;
			}
			if (marker == SdAndType.Resource)
			{
				this.m_secDescType = SecDescType.Resource;
				return;
			}
			if (marker == SdAndType.Datasource)
			{
				this.m_secDescType = SecDescType.Datasource;
				return;
			}
			if (marker == SdAndType.Model)
			{
				this.m_secDescType = SecDescType.Model;
				return;
			}
			if (marker == SdAndType.ModelItem)
			{
				this.m_secDescType = SecDescType.ModelItem;
				return;
			}
			this.m_secDescType = SecDescType.Invalid;
			throw new InternalCatalogException("Invalid security descriptor type.");
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003DC8 File Offset: 0x00001FC8
		internal static void GetRightSecDesc(SecurityItemType crtItemType, Hashtable secDescHash, out byte[] primSecDesc, out byte[] secSecDesc)
		{
			primSecDesc = null;
			secSecDesc = null;
			switch (crtItemType)
			{
			case SecurityItemType.Catalog:
			{
				SdAndType sdAndType = (SdAndType)secDescHash[SdAndType.Catalog];
				primSecDesc = sdAndType.m_secDesc;
				return;
			}
			case SecurityItemType.Folder:
			{
				SdAndType sdAndType2 = (SdAndType)secDescHash[SdAndType.Folder];
				primSecDesc = sdAndType2.m_secDesc;
				return;
			}
			case SecurityItemType.Report:
			{
				SdAndType sdAndType3 = (SdAndType)secDescHash[SdAndType.Report];
				primSecDesc = sdAndType3.m_secDesc;
				object obj = secDescHash[SdAndType.ReportSecondary];
				if (obj != null)
				{
					secSecDesc = ((SdAndType)obj).m_secDesc;
					return;
				}
				return;
			}
			case SecurityItemType.Resource:
			{
				SdAndType sdAndType4 = (SdAndType)secDescHash[SdAndType.Resource];
				primSecDesc = sdAndType4.m_secDesc;
				return;
			}
			case SecurityItemType.Datasource:
			{
				SdAndType sdAndType5 = (SdAndType)secDescHash[SdAndType.Datasource];
				primSecDesc = sdAndType5.m_secDesc;
				return;
			}
			case SecurityItemType.Model:
			{
				SdAndType sdAndType6 = (SdAndType)secDescHash[SdAndType.Model];
				primSecDesc = sdAndType6.m_secDesc;
				return;
			}
			case SecurityItemType.ModelItem:
			{
				SdAndType sdAndType7 = (SdAndType)secDescHash[SdAndType.ModelItem];
				primSecDesc = sdAndType7.m_secDesc;
				return;
			}
			default:
				throw new InternalCatalogException("Invalid security descriptor type.");
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F14 File Offset: 0x00002114
		internal static void PrepareToStoreBinaryPolicy(ArrayList secDesc, out byte[] secDescPrimary)
		{
			secDescPrimary = null;
			if (secDesc.Count == 0)
			{
				return;
			}
			int num = 1;
			num += 2;
			num += secDesc.Count * 4;
			num += secDesc.Count;
			foreach (object obj in secDesc)
			{
				SdAndType sdAndType = (SdAndType)obj;
				num += sdAndType.m_secDesc.Length;
			}
			secDescPrimary = new byte[num];
			int num2 = 0;
			secDescPrimary[num2] = Conversion.UInt32ToLowestByte((uint)secDesc.Count);
			num2++;
			secDescPrimary[num2] = Conversion.UInt32ToLowestByte(5U);
			num2++;
			secDescPrimary[num2] = Conversion.UInt32ToLowerByte(5U);
			num2++;
			foreach (object obj2 in secDesc)
			{
				SdAndType sdAndType2 = (SdAndType)obj2;
				int num3 = sdAndType2.m_secDesc.Length;
				secDescPrimary[num2] = Conversion.UInt32ToLowestByte((uint)sdAndType2.m_secDesc.Length);
				num2++;
				secDescPrimary[num2] = Conversion.UInt32ToLowerByte((uint)sdAndType2.m_secDesc.Length);
				num2++;
				secDescPrimary[num2] = Conversion.UInt32ToHigherByte((uint)sdAndType2.m_secDesc.Length);
				num2++;
				secDescPrimary[num2] = Conversion.UInt32ToHighestByte((uint)sdAndType2.m_secDesc.Length);
				num2++;
				secDescPrimary[num2] = sdAndType2.Type2Marker();
				num2++;
				sdAndType2.m_secDesc.CopyTo(secDescPrimary, num2);
				num2 += sdAndType2.m_secDesc.Length;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000040A0 File Offset: 0x000022A0
		internal static void PrepareToRestoreBinaryPolicy(byte[] secDescBlobStored, out Hashtable secDescHash)
		{
			uint num = 0U;
			byte b = secDescBlobStored[(int)num];
			num += 1U;
			byte b2 = secDescBlobStored[(int)num];
			num += 1U;
			byte b3 = secDescBlobStored[(int)num];
			num += 1U;
			uint num2 = (uint)Conversion.BytesToUInt16(b2, b3);
			if (num2 != 5U && num2 != 2U)
			{
				throw new InternalCatalogException("Incorrect security descriptor version", new object[] { secDescBlobStored });
			}
			secDescHash = new Hashtable();
			while ((ulong)num != (ulong)((long)secDescBlobStored.Length))
			{
				byte b4 = secDescBlobStored[(int)num];
				num += 1U;
				byte b5 = secDescBlobStored[(int)num];
				num += 1U;
				byte b6 = secDescBlobStored[(int)num];
				num += 1U;
				byte b7 = secDescBlobStored[(int)num];
				num += 1U;
				byte b8 = secDescBlobStored[(int)num];
				num += 1U;
				SdAndType sdAndType = new SdAndType();
				sdAndType.Marker2Type(b8);
				uint num3 = Conversion.BytesToUInt32(b4, b5, b6, b7);
				byte[] array = new byte[num3];
				Array.Copy(secDescBlobStored, (long)((ulong)num), array, 0L, (long)((ulong)num3));
				sdAndType.m_secDesc = array;
				secDescHash.Add(b8, sdAndType);
				num += num3;
			}
			if (num2 == 2U)
			{
				if (secDescHash[SdAndType.Model] != null)
				{
					throw new InternalCatalogException("Model security entry already present on upgrade!", new object[] { secDescBlobStored });
				}
				byte[] array2;
				string text;
				Native.BuildSecurityDescriptor(new WindowsAcl(), SecDescType.Model, out array2, out text);
				secDescHash.Add(SdAndType.Model, new SdAndType(SecDescType.Model, array2));
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000041D4 File Offset: 0x000023D4
		internal static void PrepareToStoreStringPolicy(ArrayList secDesc, out string secDescPrimary)
		{
			secDescPrimary = null;
			if (secDesc.Count == 0)
			{
				return;
			}
			secDescPrimary += 5U.ToString(CultureInfo.InvariantCulture);
			foreach (object obj in secDesc)
			{
				SdStrAndType sdStrAndType = (SdStrAndType)obj;
				secDescPrimary += sdStrAndType.ToString();
			}
		}

		// Token: 0x040000F7 RID: 247
		internal SecDescType m_secDescType;

		// Token: 0x040000F8 RID: 248
		internal byte[] m_secDesc;

		// Token: 0x040000F9 RID: 249
		private static byte Catalog = 1;

		// Token: 0x040000FA RID: 250
		private static byte Folder = 2;

		// Token: 0x040000FB RID: 251
		private static byte Report = 3;

		// Token: 0x040000FC RID: 252
		private static byte ReportSecondary = 4;

		// Token: 0x040000FD RID: 253
		private static byte Resource = 5;

		// Token: 0x040000FE RID: 254
		private static byte Datasource = 6;

		// Token: 0x040000FF RID: 255
		private static byte Model = 7;

		// Token: 0x04000100 RID: 256
		private static byte ModelItem = 8;
	}
}

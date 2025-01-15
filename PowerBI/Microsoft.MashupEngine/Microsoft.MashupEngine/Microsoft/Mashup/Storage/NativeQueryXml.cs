using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200207A RID: 8314
	public class NativeQueryXml : QueryPermission
	{
		// Token: 0x0600CB76 RID: 52086 RVA: 0x0028866B File Offset: 0x0028686B
		public NativeQueryXml()
		{
		}

		// Token: 0x0600CB77 RID: 52087 RVA: 0x00288673 File Offset: 0x00286873
		public NativeQueryXml(Resource resource, string nativeQueryData, int parameterCount, IEnumerable<string> parameterNames)
			: base(resource, QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted, nativeQueryData)
		{
			this.queryPermissionData = NativeQueryXml.HashQuery(nativeQueryData, parameterCount, parameterNames);
		}

		// Token: 0x17003101 RID: 12545
		// (get) Token: 0x0600CB78 RID: 52088 RVA: 0x0028868D File Offset: 0x0028688D
		// (set) Token: 0x0600CB79 RID: 52089 RVA: 0x00288695 File Offset: 0x00286895
		[XmlAttribute("NativeQuery")]
		public override string QueryPermissionData
		{
			get
			{
				return this.queryPermissionData;
			}
			set
			{
				this.queryPermissionData = value;
			}
		}

		// Token: 0x0600CB7A RID: 52090 RVA: 0x0028869E File Offset: 0x0028689E
		public override bool Matches(Resource resource, QueryPermissionChallengeType type, string nativeQueryData, int parameterCount, IEnumerable<string> parameterNames)
		{
			return base.Resource.Equals(resource) && string.CompareOrdinal(this.queryPermissionData, NativeQueryXml.HashQuery(nativeQueryData, parameterCount, parameterNames)) == 0 && base.QueryPermissionType == type;
		}

		// Token: 0x0600CB7B RID: 52091 RVA: 0x002886D0 File Offset: 0x002868D0
		private static string HashQuery(string nativeQuery, int parameterCount, IEnumerable<string> parameterNames)
		{
			string text2;
			using (HashAlgorithm hashAlgorithm = SHA256CryptoProvider.Create())
			{
				Stream stream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(nativeQuery);
				if (parameterCount > 0)
				{
					binaryWriter.Write(parameterCount);
					if (parameterNames != null)
					{
						foreach (string text in parameterNames)
						{
							binaryWriter.Write(text);
						}
					}
				}
				stream.Position = 0L;
				text2 = Convert.ToBase64String(hashAlgorithm.ComputeHash(stream));
			}
			return text2;
		}
	}
}

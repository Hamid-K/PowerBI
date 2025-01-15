using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using Microsoft.Reporting.Packaging.Internal;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000395 RID: 917
	internal sealed class ReportArchive : ReportArchiveBase
	{
		// Token: 0x06002571 RID: 9585 RVA: 0x000B3BC1 File Offset: 0x000B1DC1
		private ReportArchive(Package package)
		{
			this.m_package = package;
			base.VerifyApplicationProperties();
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000B3BD8 File Offset: 0x000B1DD8
		internal static ReportArchive Load(Stream stream)
		{
			Stream stream2 = stream;
			if (!stream.CanSeek)
			{
				stream2 = new MemoryStream();
				ReportArchiveBase.CopyStream(stream, stream2);
			}
			return new ReportArchive(Package.Open(stream2));
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000B3C08 File Offset: 0x000B1E08
		private Stream GetReportArchiveStream(string relationshipType)
		{
			using (IEnumerator<PackageRelationship> enumerator = this.m_package.GetRelationshipsByType(relationshipType).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					PackageRelationship packageRelationship = enumerator.Current;
					Uri uri = PackUriHelper.ResolvePartUri(new Uri("/", UriKind.Relative), packageRelationship.TargetUri);
					if (this.m_package.PartExists(uri))
					{
						return this.m_package.GetPart(uri).GetStream();
					}
					throw new ReportArchiveException(RPRes.rsNullReportArchiveStream);
				}
			}
			return null;
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000B3C9C File Offset: 0x000B1E9C
		protected override Stream GetApplicationPropertiesStream()
		{
			return this.GetReportArchiveStream("http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportpackage/relationships/extendedproperties");
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000B3CAC File Offset: 0x000B1EAC
		protected override void HandleApplicationPropertiesError(ReportArchiveBase.ApplicationPropertiesError error, params string[] items)
		{
			switch (error)
			{
			case ReportArchiveBase.ApplicationPropertiesError.InvalidAppPropsRootElement:
				throw new ReportArchiveException(RPRes.rsInvalidAppPropsRootElement);
			case ReportArchiveBase.ApplicationPropertiesError.UndefinedMustUnderstandNamespaces:
				RSTrace.ProcessingTracer.Assert(items.Length != 0, "Missing parameter item");
				throw new ReportArchiveException(RPResWrapper.rsUndefinedMustUnderstandNamespaces(items[0]));
			case ReportArchiveBase.ApplicationPropertiesError.UnrecognizedNonIgnorableNamespaces:
				RSTrace.ProcessingTracer.Assert(items.Length != 0, "Missing parameter item");
				throw new ReportArchiveException(RPResWrapper.rsUnrecognizedNonIgnorableNamespaces(items[0]));
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000B3D21 File Offset: 0x000B1F21
		internal override Stream GetReportDefinitionStream()
		{
			return this.GetReportArchiveStream("http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/reportdefinition");
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000B3D30 File Offset: 0x000B1F30
		internal Dictionary<string, Stream> GetSectionPreviewLocationsMap()
		{
			PackagePart packagePart = null;
			Uri uri = null;
			foreach (PackageRelationship packageRelationship in this.m_package.GetRelationshipsByType("http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/reportdefinition"))
			{
				uri = PackUriHelper.ResolvePartUri(new Uri("/", UriKind.Relative), packageRelationship.TargetUri);
				if (this.m_package.PartExists(uri))
				{
					packagePart = this.m_package.GetPart(uri);
				}
			}
			Dictionary<string, Stream> dictionary = new Dictionary<string, Stream>();
			foreach (PackageRelationship packageRelationship2 in packagePart.GetRelationshipsByType("http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/sectionpreviewimage"))
			{
				Uri uri2 = PackUriHelper.ResolvePartUri(uri, packageRelationship2.TargetUri);
				if (this.m_package.PartExists(uri2))
				{
					PackagePart part = this.m_package.GetPart(uri2);
					dictionary.Add(packageRelationship2.Id, part.GetStream());
				}
			}
			return dictionary;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000B3E3C File Offset: 0x000B203C
		internal void Close()
		{
			if (this.m_package != null)
			{
				this.m_package.Close();
				this.m_package = null;
			}
		}

		// Token: 0x040015DC RID: 5596
		private Package m_package;
	}
}

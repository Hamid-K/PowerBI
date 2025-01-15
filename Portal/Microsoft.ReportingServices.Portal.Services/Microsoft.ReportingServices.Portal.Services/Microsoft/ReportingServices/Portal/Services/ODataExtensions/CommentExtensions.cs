using System;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.Exceptions;
using Microsoft.ReportingServices.Library;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200003B RID: 59
	internal static class CommentExtensions
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000FC24 File Offset: 0x0000DE24
		public static global::Model.Comment ToOdataModel(this CommentEntity comment)
		{
			global::Model.Comment comment2 = new global::Model.Comment();
			comment2.Id = comment.CommentId;
			comment2.ItemId = new Guid?(comment.ItemId);
			comment2.UserName = comment.UserName ?? ErrorStrings.UserNameUnknown;
			comment2.ThreadId = comment.ThreadId;
			comment2.Text = comment.Text;
			comment2.CreatedDate = comment.CreatedDate;
			DateTime? modifiedDate = comment.ModifiedDate;
			comment2.ModifiedDate = ((modifiedDate != null) ? new DateTimeOffset?(modifiedDate.GetValueOrDefault()) : null);
			comment2.AttachmentPath = comment.AttachmentPath;
			return comment2;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		public static global::Model.Comment ToOdataModel(this Microsoft.ReportingServices.Library.Comment comment)
		{
			global::Model.Comment comment2 = new global::Model.Comment();
			comment2.Id = comment.Id;
			comment2.ItemId = new Guid?(comment.ItemId);
			comment2.UserName = comment.UserName;
			comment2.ThreadId = comment.ThreadId;
			comment2.Text = comment.Text;
			comment2.CreatedDate = comment.CreatedDate;
			DateTime? modifiedDate = comment.ModifiedDate;
			comment2.ModifiedDate = ((modifiedDate != null) ? new DateTimeOffset?(modifiedDate.GetValueOrDefault()) : null);
			comment2.AttachmentPath = comment.AttachmentPath;
			return comment2;
		}
	}
}

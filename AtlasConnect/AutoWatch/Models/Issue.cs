using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoWatch.Models
{
    public class Issue
    {
        public static readonly string JIRA_DATE_TIME_PATTERN = "yyyy-MM-dd'T'HH:mm:ss.SSSZ";
        public static readonly string READABLE_DATE_TIME_PATTERN = "dd/MMM/yy K:mm a";

        private readonly long id;
        private readonly string description;
        private readonly string key;
        private readonly string summary;
        private readonly DateTime createdDate;
        private readonly IconisedIssueField issueType;
        private readonly IconisedIssueField priority;
        private readonly Project project;
        private readonly UserIssueField assignee;
        private readonly UserIssueField reporter;

        private Issue(long id, string key, string summary, Project project, string description, IconisedIssueField type, UserIssueField reporter, UserIssueField assignee, IconisedIssueField priority, DateTime createdDate)
        {
            this.id = id;
            this.key = key;
            this.summary = summary;
            this.project = project;
            this.description = description;
            this.issueType = type;
            this.reporter = reporter;
            this.assignee = assignee;
            this.priority = priority;
            this.createdDate = createdDate;
        }

        public string getUrl()
        {
            return "";
        }

        public long getId()
        {
            return id;
        }

        public string getKey()
        {
            return key;
        }

        public Project getProject()
        {
            return project;
        }

        public UserIssueField getReporter()
        {
            return reporter;
        }

        public UserIssueField getAssignee()
        {
            return assignee;
        }

        public DateTime getCreatedDate()
        {
            return createdDate;
        }

        public String getDescription()
        {
            return description;
        }

        public String getSummary()
        {
            return summary;
        }

        public IconisedIssueField getPriority()
        {
            return priority;
        }

        public IconisedIssueField getIssueType()
        {
            return issueType;
        }

        public static Issue toIssue(JObject issueJson)
        {
            long issueId = issueJson.SelectToken("id").ToObject<long>();
            string key = SaveText(issueJson, "key");

            JObject fields = issueJson.SelectToken("fields").ToObject<JObject>();

            long projectId = fields.SelectToken("project").SelectToken("id").ToObject<long>();
            string projectKey = fields.SelectToken("project").SelectToken("key").ToString();
            string projectName = fields.SelectToken("project").SelectToken("name").ToString();
            Project project = new Project(projectId, projectKey, projectName);

            string summary = SaveText(fields, "summary");
            string desc = SaveText(fields, "description");

            JToken type = fields.SelectToken("issuetype");
            string typeName = SaveText(type, "name");
            Uri typeIconUrl = new Uri(SaveText(type, "iconUrl"));
            IconisedIssueField issueType = new IconisedIssueField(typeName, typeIconUrl);

            JObject reporter = fields.SelectToken("reporter").ToObject<JObject>();
            UserIssueField issueReporter;
            if (reporter.HasValues)
            {
                string reporterName = SaveText(reporter, "name");
                string reporterDisplayName = SaveText(reporter, "displayName");
                Uri reporterAvatarUrl = new Uri(reporter.SelectToken("avatarUrls").SelectToken("32x32").ToString());
                issueReporter = new UserIssueField(reporterName, reporterDisplayName, reporterAvatarUrl);
            }
            else
            {
                issueReporter = null;
            }

            JToken priority = fields.SelectToken("priority"); ;
            string priorityName = priority.SelectToken("name").ToString();
            Uri priorityIconUrl = new Uri(priority.SelectToken("iconUrl").ToString());
            IconisedIssueField issuePriority = new IconisedIssueField(priorityName, priorityIconUrl);

            JToken assignee = fields.SelectToken("assignee");
            UserIssueField issueAssignee;
            if (assignee.HasValues)
            {
                string assigneeName = assignee.SelectToken("name").ToString();
                string assigneeDisplayName = assignee.SelectToken("displayName").ToString();
                Uri assigneeAvatarUrl = new Uri(assignee.SelectToken("avatarUrls").SelectToken("32x32").ToString());
                issueAssignee = new UserIssueField(assigneeName, assigneeDisplayName, assigneeAvatarUrl);
            }
            else
            {
                issueAssignee = null;
            }

            DateTime createdDate = DateTime.Parse(SaveText(fields, "created"));
            return new Issue(issueId, key, summary, project, desc, issueType, issueReporter, issueAssignee, issuePriority, createdDate);
        }

        private static string SaveText(JToken node, string field)
        {
            if (node.HasValues.Equals(field) && node.SelectToken(field).GetType() == typeof(String))
            {
                return node.SelectToken(field).ToString();
            }
            
            return "";
        }

        public class Project
        {
            private readonly string key;
            private readonly string name;
            private readonly long id;

            public Project(long id, string key, string name)
            {
                this.key = key;
                this.name = name;
                this.id = id;
            }

            public string getKey()
            {
                return key;
            }

            public string getName()
            {
                return name;
            }

            public long getId()
            {
                return id;
            }
        }

        public class IconisedIssueField
        {
            private readonly string name;
            private readonly Uri iconUrl;

            public IconisedIssueField(string name, Uri iconUrl)
            {
                this.name = name;
                this.iconUrl = iconUrl;
            }

            public string getName()
            {
                return name;
            }

            public Uri getIconUrl()
            {
                return iconUrl;
            }
        }

        public class UserIssueField
        {
            private readonly string username;
            private readonly string displayName;
            private readonly Uri iconUrl;

            public UserIssueField(string username, string displayName, Uri iconUrl)
            {
                this.username = username;
                this.displayName = displayName;
                this.iconUrl = iconUrl;
            }

            public string getUsername()
            {
                return username;
            }

            public string getDispayName()
            {
                return displayName;
            }

            public Uri getIconUrl()
            {
                return iconUrl;
            }
        }
    }
}
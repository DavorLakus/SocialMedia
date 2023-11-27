// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Group ID mapping
var groupIdMapping = {
    "EPL": 230416,
    "DFL": 215519,
    "Bundesliga 1": 228717,
    "Bundesliga 2": 228718
};

// Accessing group ID by name
function getGroupId(groupName) {
    return groupIdMapping[groupName] || null;
}

// Accessing group name by ID
function getGroupName(groupId) {
    for (var name in groupIdMapping) {
        if (groupIdMapping.hasOwnProperty(name) && groupIdMapping[name] === groupId) {
            return name;
        }
    }
    return null;
}

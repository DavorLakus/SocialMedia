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

// Tag ID mapping
var tagIdMapping = {
    14549: "DFL YES",
    14550: "NO DFL",
    16800: "EPL YES",
    16801: "NO EPL",
    17234: "Handball",
    17235: "Basketball",
    17236: "Tischtennis",
    17237: "Volleyball",
    17238: "Hockey"
};

// Accessing tag name by ID
function getTagName(tagId) {
    return tagIdMapping[tagId] || null;
}

// Accessing tag ID by name
function getTagId(tagName) {
    for (var id in tagIdMapping) {
        if (tagIdMapping.hasOwnProperty(id) && tagIdMapping[id] === tagName) {
            return id;
        }
    }
    return null;
}

var options = [{
    value: 0,
    text: 'enhancement'
}, {
    value: 1,
    text: 'bug'
}, {
    value: 2,
    text: 'duplicate'
}, {
    value: 3,
    text: 'invalid'
}]
var select6 = document.getElementById('select-6');
var select6c = new coreui.MultiSelect(select6, {
    inline: true,
    multiple: true,
    search: true,
    options: options,
    selectionType: 'tags',
    searchPlaceholder: 'Search'
});

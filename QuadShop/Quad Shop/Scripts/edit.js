const input = document.getElementById('image_uploads');
const iii = document.getElementById('edit_img');
input.style.opacity = 0;
input.addEventListener('change', updateImageDisplay);
function updateImageDisplay() {
    const curFiles = input.files;
    for (const file of curFiles) {
        if (validFileType(file)) {
            iii.src = URL.createObjectURL(file);
        }
    }
}
const fileTypes = [
    'image/jpeg',
    'image/pjpeg',
    'image/png'
];

function validFileType(file) {
    return fileTypes.includes(file.type);
}
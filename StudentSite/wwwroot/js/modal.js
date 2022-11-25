function openModalUser(parameters) {
    const coolData = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (coolData === undefined || url === undefined) {
        alert('Упссс.... что-то пошло не так')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: {
            "userString": coolData
        },
        success: function (response) {
            modal.find(".modal-content").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};
function openModalNote(parameters) {
    const coolData = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (coolData === undefined || url === undefined) {
        alert('Упссс.... что-то пошло не так')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: {
            "noteString": coolData
        },
        success: function (response) {
            modal.find(".modal-content").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};
function openModal(parameters) {
    const coolData = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (coolData === undefined || url === undefined) {
        alert('Упссс.... что-то пошло не так')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: {
            "hwString": coolData
        },
        success: function (response) {
            modal.find(".modal-content").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};


// изменение таски 
function edit(event) {
    event.preventDefault();

    $("#stateTextBox").val('editTask');

    var id = $(this).attr('id').replace("editTaskLink_", "");
    $("#idTextBox").val(id);

    var data = $("#addTaskForm").serialize();
    var url = $("#addTaskForm").attr('action');

    $.post(url, data, function (response) {
        $('#tasksTable').replaceWith(response);
        $('.deleteTaskLink').click(del);
        $('.editTaskLink').click(edit);
        $('.incPriorityLink').click(increasePriority);
        $('.decPriorityLink').click(decreasePriority);
    });
}


//удаление таски
function del(event) {
    event.preventDefault();

    $("#stateTextBox").val('deleteTask');

    var id = $(this).attr('id').replace("deleteTaskLink_", "");
    $("#idTextBox").val(id);

    var data = $("#addTaskForm").serialize();
    var url = $("#addTaskForm").attr('action');

    $.post(url, data, function (response) {
        $('#tasksTable').replaceWith(response);
        $('.deleteTaskLink').click(del);
        $('.editTaskLink').click(edit);
        $('.incPriorityLink').click(increasePriority);
        $('.decPriorityLink').click(decreasePriority);
    });
}


//увеличение приоритета
function increasePriority(event) {
    event.preventDefault();
    $("#stateTextBox").val('increasePriority');

    var id = $(this).attr('id').replace("incPriorityLink_", "");
    $("#idTextBox").val(id);

    
    var data = $("#addTaskForm").serialize();
    var url = $("#addTaskForm").attr('action');

    $.post(url, data, function (response) {
        $('#tasksTable').replaceWith(response);
        $('.deleteTaskLink').click(del);
        $('.editTaskLink').click(edit);
        $('.incPriorityLink').click(increasePriority);
        $('.decPriorityLink').click(decreasePriority);
    });

}

//уменьшение приоритета
function decreasePriority(event) {
    event.preventDefault();
    $("#stateTextBox").val('decreasePriority');

    var id = $(this).attr('id').replace("decPriorityLink_", "");
    $("#idTextBox").val(id);


    var data = $("#addTaskForm").serialize();
    var url = $("#addTaskForm").attr('action');

    $.post(url, data, function (response) {
        $('#tasksTable').replaceWith(response);
        $('.deleteTaskLink').click(del);
        $('.editTaskLink').click(edit);
        $('.incPriorityLink').click(increasePriority);
        $('.decPriorityLink').click(decreasePriority);
    });
}

// добавление таски
$(document).ready(function () {
    $('#addTaskForm').submit(function (event) {
        event.preventDefault();

        $("#stateTextBox").val('addTask');

        var data = $(this).serialize();
        var url = $(this).attr('action');

        $.post(url, data, function (response) {
            $('#tasksTable').replaceWith(response);
            $('.deleteTaskLink').click(del);
            $('.editTaskLink').click(edit);
            $('.incPriorityLink').click(increasePriority);
            $('.decPriorityLink').click(decreasePriority);
        });
       
    });
});


// функция для применения изменений того что вы там наредактировали в поле текста задания
function editFunction(event) {
    event.preventDefault();

    $("#stateTextBox").val('editTaskText');
    $("#addTask").val($('#editTextBox').attr('value'));
      
    
    var data = $("#addTaskForm").serialize();
    var url = $("#addTaskForm").attr('action');

    $.post(url, data, function (response) {
        $('#tasksTable').replaceWith(response);
        $('.deleteTaskLink').click(del);
        $('.editTaskLink').click(edit);
        $('.incPriorityLink').click(increasePriority);
        $('.decPriorityLink').click(decreasePriority);
    });

    $('#addTask').val("");
}


//собственно привязка к событиям вышеперечисленных функций
$(document).ready(function () {
    $('.deleteTaskLink').click(del);
});

$(document).ready(function () {
    $('.editTaskLink').click(edit);
});

$(document).ready(function () {
    $('.incPriorityLink').click(increasePriority);
});

$(document).ready(function () {
    $('.decPriorityLink').click(decreasePriority);
});

$('.charity-alphabetical').change(function () {
	var n, t;
	$(this).val() != ''
		? ((n = $(this).val() == '#' ? '#\\' + this.value : '#' + this.value),
			$('.refresh-btn').show())
		: (n = '.letter-container:not(.letter-container-empty)'),
		(t = $(n)),
		$('.letter-container')
			.not(t)
			.hide(600),
		t.show(600);
});


window.onload = function () {
	var selItem = sessionStorage.getItem("SelItem");
	if (selItem == null) {
		selItem = 'select';
	} else {
		$('.charity-type').val(selItem);
	}
	
}

$('.charity-type').change(function () {
	var selVal = $(this).val();
	sessionStorage.setItem("SelItem", selVal);
});

$('.directory-list').each(function () {
	if ($.trim($(this).text()) == '' && $(this).children().length == 0) {
		$(this).parent().hide();
		$(this).html('<li>No charities found for chosen filter.</li>');
	}
});


document.getElementById("refresh-btn").onclick = function () {
	$('.charity-type').val('select').trigger('change');
};



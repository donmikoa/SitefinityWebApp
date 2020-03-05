$('.charity-type option:selected').index() > 0 &&
	$('.refresh-btn').show(),
	$('.charity-type').change(function () {
		var n = document.URL,
			t = n.substr(n.lastIndexOf('/') + 1);
		$(".charity-type option[value='" + t + "']").length > 0 &&
			(n = n.replace(t, '')),
			(n = n.replace(/\/?$/, '/')),
			(window.location.href = n + this.value);
	}),
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
	})
	